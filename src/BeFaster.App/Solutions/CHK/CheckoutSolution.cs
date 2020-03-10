using BeFaster.App.Solutions.CHK.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static readonly IList<Product> Products = new List<Product>();
        public static readonly IDictionary<char, SpecialOffer> SpecialOffers = new Dictionary<char, SpecialOffer>();
        private const int invalidInput = -1;

        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }

            if (Products.Count == 0) return invalidInput;

            int totalPrice = CalculateTotalPrice(skus);
            int totalDiscount = CalculateDiscount(skus);

            return totalPrice;
        }

        private static int CalculateDiscount(string skus)
        {
            int discount = 0;
            foreach (char sku in skus)
            {
                SpecialOffers.ContainsKey(sku)
             }

            return discount;
        }

        private static int CalculateTotalPrice(string skus)
        {
            int totalPrice = 0;
            foreach (char sku in skus)
            {
                var product = Products.FirstOrDefault(x => x.Id == sku);                
                if (product != null)
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


