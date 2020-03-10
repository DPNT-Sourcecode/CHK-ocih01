using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK.Services
{
    public class SpecialOfferService
    {
        public static readonly IDictionary<char, IList<ISpecialOffer>> SpecialOffers = new Dictionary<char, IList<ISpecialOffer>>();

        public static int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;
            var specialOffers = SpecialOffers[productId];
            bool isFirstPriceCalculation = true;

            foreach (var offer in specialOffers)
            {
                if (offer is BuyMultipleForPriceReduction multiplePriceReductionOffer)
                {
                    int calculatedDiscountedPrice = multiplePriceReductionOffer.GetDiscountedPrice(productId, cartItemQuantity, actualProductPrice);
                    discountedPrice = isFirstPriceCalculation || calculatedDiscountedPrice < discountedPrice ? calculatedDiscountedPrice : discountedPrice;
                    isFirstPriceCalculation = false;
                }
            }
            return discountedPrice;
        }


        public static IDictionary<char, int> ApplyBuyOneProductGetAnotherProductFreeOffer(IDictionary<char, int> skuCounts)
        {
            var offers = SpecialOffers.Where(x => x.Value.Any(y => y.GetType().Equals(typeof(BuyOneGetAnotherFree))))
                .ToDictionary(s => s.Key, s => s.Value.Where(z => z.OfferType == Enums.SpecialOfferType.BuyOneGetAnotherFree).ToList());

            foreach (var offer in offers)
            {
                if (skuCounts.Keys.Contains(offer.Key))
                {
                    foreach (BuyOneGetAnotherFree buyOneGetOneOffer in offer.Value)
                    {
                        if (skuCounts[offer.Key] >= buyOneGetOneOffer.ItemQuantity && skuCounts.Keys.Contains(buyOneGetOneOffer.FreeItemId))
                        {
                            var numberOFItemsToReduce = (skuCounts[offer.Key] / buyOneGetOneOffer.ItemQuantity) * buyOneGetOneOffer.FreeItemQuantity;
                            int itemCountAfterReduction = skuCounts[buyOneGetOneOffer.FreeItemId] - numberOFItemsToReduce;
                            skuCounts[buyOneGetOneOffer.FreeItemId] = itemCountAfterReduction > 0 ? itemCountAfterReduction : 0;
                        }
                    }
                }
            }

            return skuCounts;
        }
    }
}

