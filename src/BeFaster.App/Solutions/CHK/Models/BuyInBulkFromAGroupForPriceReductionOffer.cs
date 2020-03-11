using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyInBulkFromAGroupForPriceReductionOffer : SpecialOffer
    {
        public List<char> Products { get; set; }
        public int SpecialPrice { get; set; }
    }
}


