using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static readonly IList<Product> Products = new List<Product>();
        public static readonly IDictionary<char, SpecialOffer> SpecialOffers = new Dictionary<char, SpecialOffer>();

        public static int ComputePrice(string skus)
        {
            const int invalidInput = -1;
            int totalPrice = 0;

            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }

            if (Products.Count == 0) return invalidInput;

            foreach(char sku in skus)
            {
                var product = Products.FirstOrDefault(x => x.Id == sku);
                if(product != null)
                {
                    totalPrice += product.Price;
                }
                else
                {
                    return invalidInput;
                }
            }
            return totalPrice;
        }
    }
}


