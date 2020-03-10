using BeFaster.App.Solutions.CHK.Models;
using BeFaster.Runner.Exceptions;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution
    {
        public IList<Product> Products;
        public IDictionary<int, SpecialOffer> specialOffers;

        public int ComputePrice(string skus)
        {
            const int invalidInput = -1;

            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }

            return 0;
        }
    }
}
