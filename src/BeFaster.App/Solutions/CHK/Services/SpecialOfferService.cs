using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK.Services
{
    public class SpecialOfferService : ISpecialOfferService
    {
        public int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice, IEnumerable<SpecialOffer> specialOffers)
        {
            int discountedPrice = 0;
            specialOffers = specialOffers.ToList().OrderByDescending(x=>x.ItemQuantity);

            foreach (BuyMultipleForPriceReduction offer in specialOffers)
            {
                if (cartItemQuantity < offer.ItemQuantity) continue;

                discountedPrice += offer.GetDiscountedPrice(productId, cartItemQuantity, actualProductPrice);
                cartItemQuantity = cartItemQuantity - (offer.ItemQuantity * (cartItemQuantity / offer.ItemQuantity));
                if (cartItemQuantity == 0) break;                   
            }

            if (cartItemQuantity > 0)
            {
                discountedPrice += cartItemQuantity * actualProductPrice;
            }
            
            return discountedPrice;
        }


        public IDictionary<char, int> ApplyBuyOneProductGetAnotherProductFreeOffer(IDictionary<char, int> skuCounts, Dictionary<char, List<SpecialOffer>> specialOffers)
        {
            foreach (var offer in specialOffers)
            {
                if (skuCounts.Keys.Contains(offer.Key))
                {
                    foreach (BuyOneGetAnotherFree buyOneGetOneOffer in offer.Value)
                    {
                        if (skuCounts[offer.Key] >= buyOneGetOneOffer.ItemQuantity && skuCounts.Keys.Contains(buyOneGetOneOffer.FreeItemId))
                        {
                            if (offer.Key == buyOneGetOneOffer.FreeItemId)
                            {
                                skuCounts = ApplyBuyOneProductGetSameProductFreeOffer(skuCounts, buyOneGetOneOffer);
                            }
                            else
                            {
                                var numberOfItemsToReduce = (skuCounts[offer.Key] / buyOneGetOneOffer.ItemQuantity) * buyOneGetOneOffer.FreeItemQuantity;
                                int itemCountAfterReduction = skuCounts[buyOneGetOneOffer.FreeItemId] - numberOfItemsToReduce;
                                skuCounts[buyOneGetOneOffer.FreeItemId] = itemCountAfterReduction > 0 ? itemCountAfterReduction : 0;
                            }
                        }
                    }
                }
            }

            return skuCounts;
        }

        private IDictionary<char, int> ApplyBuyOneProductGetSameProductFreeOffer(IDictionary<char, int> skuCounts, BuyOneGetAnotherFree buyOneGetOneOffer)
        {
            int productItemCount = skuCounts[buyOneGetOneOffer.FreeItemId];
            while (productItemCount >= buyOneGetOneOffer.ItemQuantity)
            {
                productItemCount -= buyOneGetOneOffer.ItemQuantity;
                if(productItemCount >= buyOneGetOneOffer.FreeItemQuantity)
                {
                    productItemCount -= buyOneGetOneOffer.FreeItemQuantity;
                    skuCounts[buyOneGetOneOffer.FreeItemId] -= buyOneGetOneOffer.FreeItemQuantity;
                }
            }
            return skuCounts;
        }
    }
}

