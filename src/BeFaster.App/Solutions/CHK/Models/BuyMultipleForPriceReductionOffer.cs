namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyMultipleForPriceReductionOffer : SpecialOffer
    {
        public int SpecialPrice { get; set; }

        public int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            return cartItemQuantity / ItemQuantity * SpecialPrice;
        }
    }
}