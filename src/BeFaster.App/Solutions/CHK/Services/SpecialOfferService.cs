using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

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
    }
}
