using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Tests.Solutions.CHK.TestModels
{
    public class TestProduct
    {
        public char Id { get; set; }
        public int Price { get; set; }
        public IList<BuyMultipleProductsForPriceReductionOffer> BuyMultipleForPriceReductionOffers { get; set; }
        public BuyOneGetAnotherFreeOffer BuyOneGetAnotherFreeOffer { get; set; }
    }
}
