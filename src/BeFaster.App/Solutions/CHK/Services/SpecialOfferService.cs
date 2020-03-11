using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK.Services
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly ISpecialOffersRepository specialOffersRepository;

        public SpecialOfferService(ISpecialOffersRepository specialOffersRepository)
        {
            this.specialOffersRepository = specialOffersRepository;
        }

        public int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            var offers = specialOffersRepository.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>().
                Where(x=>x.ProductId == productId).
                OrderByDescending(x => x.ItemQuantity).ToList();

             return offers != null && offers.Any() ?
                        GetDiscountedPrice(offers, cartItemQuantity, actualProductPrice)
                        : actualProductPrice * cartItemQuantity;
        }

        private static int GetDiscountedPrice(List<BuyMultipleProductsForPriceReductionOffer> specialOffers, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;

            foreach (BuyMultipleProductsForPriceReductionOffer offer in specialOffers)
            {
                if (cartItemQuantity < offer.ItemQuantity) continue;

                discountedPrice += (cartItemQuantity / offer.ItemQuantity) * offer.SpecialPrice;
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

        public IDictionary<char, int> ApplyBuyGroupOfProductsForPriceReductionOffer(IDictionary<char, int> skuCounts, IDictionary<char, Product> products)
        {
            var offers = specialOffersRepository.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>().Where(x=>x.IsGroupingAllowed)
                .GroupBy(y=>y.OfferId).Select(x=>x.First()).ToList();

            foreach (var offer in offers)
            {
                var skusThatMatchOffer = skuCounts.Where(y => offer.CombinationProducts.Contains(y.Key)).ToList();
                if (!skusThatMatchOffer.Any() || skusThatMatchOffer.Count == 1) { continue; }
                var sumOfSkusThatMatchOffer = skuCounts.Where(y => offer.CombinationProducts.Contains(y.Key)).Sum(x => x.Value);
                if (sumOfSkusThatMatchOffer < offer.ItemQuantity) continue;


                var noOfProductsToDistribute = sumOfSkusThatMatchOffer % offer.ItemQuantity;


                if(noOfProductsToDistribute == 0)
                {
                    skuCounts[skusThatMatchOffer[0].Key] = sumOfSkusThatMatchOffer;
                    for(int i= 1; i<skusThatMatchOffer.Count; i++)
                    {
                        skuCounts[skusThatMatchOffer[i].Key] = 0;
                    }
                    continue;
                }

                skuCounts = ApplyDistributionToSkuCounts(skuCounts, products, offer, sumOfSkusThatMatchOffer);
            }
           
            return skuCounts;
        }

        private IDictionary<char, int> ApplyDistributionToSkuCounts(IDictionary<char, int> skuCounts, IDictionary<char, Product> products, 
            BuyMultipleProductsForPriceReductionOffer offer, int sumOfSkusThatMatchOffer)
        {
            var groupedProducts = products.Where(x => offer.CombinationProducts.Contains(x.Key)).OrderByDescending(x => x.Value.Price).ToList();

            bool isFirstProduct = true;
            int productsToGroupWithOther = 0;
            char previousProductId = groupedProducts[0].Key;

            foreach (var product in groupedProducts)
            {                
                if (!skuCounts.ContainsKey(product.Key))
                {
                    continue;
                }
                if (isFirstProduct)
                {
                    productsToGroupWithOther = (skuCounts[product.Key]) % offer.ItemQuantity;
                    skuCounts[product.Key] -= productsToGroupWithOther;                    
                    isFirstProduct = false;
                }
                else if(productsToGroupWithOther > 0 ||  sumOfSkusThatMatchOffer >= offer.ItemQuantity)
                {
                    skuCounts[product.Key] = productsToGroupWithOther + skuCounts[product.Key];
                    productsToGroupWithOther = skuCounts[product.Key] % offer.ItemQuantity;
                    skuCounts[product.Key] -= productsToGroupWithOther;
                }
                sumOfSkusThatMatchOffer = sumOfSkusThatMatchOffer - skuCounts[product.Key];
                previousProductId = product.Key;
            }

            skuCounts[previousProductId] = skuCounts[previousProductId] + productsToGroupWithOther;

            return skuCounts;
        }
    }
}

