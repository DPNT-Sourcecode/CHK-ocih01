using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Tests.Solutions.CHK.TestModel
{
    public class TestProduct
    {
        public char Id { get; set; }
        public int Price { get; set; }
        public IList<BuyMultipleOfSameForPriceReductionOffer> BuyMultipleForPriceReductionOffers { get; set; }
        public BuyOneGetAnotherFreeOffer BuyOneGetAnotherFreeOffer { get; set; }
    }
}

