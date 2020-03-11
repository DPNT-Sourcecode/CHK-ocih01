using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyMultipleOfSameForPriceReductionOffer : SpecialOffer
    {
        public char ProductId { get; set; }
        public bool IsGroupingAllowed { get; set; }
        public IList<char> CombinationProducts { get; set; }

        public int SpecialPrice { get; set; }

        public int GetDiscountedPrice(int cartItemQuantity, int actualProductPrice)
        {
            return cartItemQuantity / ItemQuantity * SpecialPrice;
        }
    }
}

