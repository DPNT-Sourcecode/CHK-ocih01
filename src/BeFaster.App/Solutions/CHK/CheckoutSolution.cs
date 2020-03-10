using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static readonly IList<Product> Products = new List<Product>();
        public static readonly IDictionary<int, SpecialOffer> SpecialOffers = new Dictionary<int, SpecialOffer>();

        public static int ComputePrice(string skus)
        {
            const int invalidInput = -1;

            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }

            if (Products.Count == 0) return invalidInput;

            return 0;
        }
    }
}




