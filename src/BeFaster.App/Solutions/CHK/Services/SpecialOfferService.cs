using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using BeFaster.App.Solutions.CHK.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK.Services
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private static readonly ISpecialOffersRepository specialOffersRepository = new SpecialOffersRepository();

        public int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            var buyMultipleOfSameForPriceReductionOffer = specialOffersRepository.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>().
                Where(x=>x.ProductId == productId).
                OrderByDescending(x => x.ItemQuantity).ToList();

             return buyMultipleOfSameForPriceReductionOffer != null && buyMultipleOfSameForPriceReductionOffer.Any() ?
                        GetDiscountedPrice(buyMultipleOfSameForPriceReductionOffer, cartItemQuantity, actualProductPrice)
                        : actualProductPrice * cartItemQuantity;
        }

        private static int GetDiscountedPrice(List<BuyMultipleOfSameForPriceReductionOffer> specialOffers, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;

            foreach (BuyMultipleOfSameForPriceReductionOffer offer in specialOffers)
            {
                if (cartItemQuantity < offer.ItemQuantity) continue;

                discountedPrice += offer.GetDiscountedPrice(cartItemQuantity, actualProductPrice);
                cartItemQuantity = cartItemQuantity - (offer.ItemQuantity * (cartItemQuantity / offer.ItemQuantity));
                if (cartItemQuantity == 0) break;
            }
            if (cartItemQuantity > 0)
            {
                discountedPrice += cartItemQuantity * actualProductPrice;
            }
            return discountedPrice;
        }


        public IDictionary<char, int> ApplyBuyOneProductGetAnotherProductFreeOffer(IDictionary<char, int> skuCounts)
        {
            var buyOneGetAnotherFreeOffers = specialOffersRepository.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>().ToDictionary(x => x.ProductId, x => x);
            foreach (var offer in buyOneGetAnotherFreeOffers)
            {
                if (skuCounts.Keys.Contains(offer.Key))
                {
                    var buyOneGetOneOffer = offer.Value;
                    if (skuCounts[offer.Key] >= buyOneGetOneOffer.ItemQuantity && skuCounts.Keys.Contains(buyOneGetOneOffer.FreeItemId))
                    {
                        //Check if the free item is same or not
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
            return skuCounts;
        }

        private IDictionary<char, int> ApplyBuyOneProductGetSameProductFreeOffer(IDictionary<char, int> skuCounts, BuyOneGetAnotherFreeOffer buyOneGetOneOffer)
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

