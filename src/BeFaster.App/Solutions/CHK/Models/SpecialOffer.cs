using BeFaster.App.Solutions.CHK.Enums;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class SpecialOffer
    {
        public int ItemQuantity { get; set; }
        public SpecialOfferType OfferType{ get; set; }
    }

    public class BuyOneGetAnotherFree : SpecialOffer
    {
        public char FreeItemId { get; set; }
    }

    public class BuyMultipleForPriceReduction : SpecialOffer
    {
        public int SpecialPrice { get; set; }

        public int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;

            discountedPrice = cartItemQuantity / ItemQuantity * SpecialPrice;
            discountedPrice += cartItemQuantity % ItemQuantity * actualProductPrice;

            return discountedPrice;
        }
    }
}

