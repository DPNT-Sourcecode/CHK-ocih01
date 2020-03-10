using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution
    {
        private readonly IList<Product> products = new List<Product>();
        private readonly IDictionary<int, SpecialOffer> specialOffers = new Dictionary<int, SpecialOffer>();

        public CheckoutSolution(IList<Product> products, IDictionary<int, SpecialOffer> specialOffers)
        {
            this.products = products;
            this.specialOffers = specialOffers;
        }

        public static int ComputePrice(string skus)
        {
            const int invalidInput = -1;

            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }



            return 0;
        }
    }
}



