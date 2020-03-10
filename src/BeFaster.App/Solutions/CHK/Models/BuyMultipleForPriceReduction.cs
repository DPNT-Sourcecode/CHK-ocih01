namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyMultipleForPriceReduction : SpecialOffer
    {
        public int SpecialPrice { get; set; }

        public int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;

            discountedPrice = cartItemQuantity / ItemQuantity * SpecialPrice;

            return discountedPrice;
        }
    }
}
