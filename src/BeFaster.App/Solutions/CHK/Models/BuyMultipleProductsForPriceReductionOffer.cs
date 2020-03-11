using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyMultipleProductsForPriceReductionOffer : SpecialOffer
    {
        public char ProductId { get; set; }
        public int SpecialPrice { get; set; }
        public bool IsGroupingAllowed { get; set; }
        public List<char> CombinationProducts { get; set; }
        public int OfferId { get; set; }
    }
}