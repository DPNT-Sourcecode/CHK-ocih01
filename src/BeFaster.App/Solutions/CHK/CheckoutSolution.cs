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
        private static IDictionary<char, int> skuCounts = new Dictionary<char, int>();
        private const int invalidInput = -1;

        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }

            if (Products.Count == 0) return invalidInput;

            CountSkus(skus);

            int totalPrice = CalculateTotalPrice(skus);
            int totalDiscount = CalculateDiscount(skus);

            return totalPrice;
        }

        private static void CountSkus(string skus)
        {
            foreach (char sku in skus)
            {
                if(skuCounts.ContainsKey(sku))
                {
                    skuCounts[sku] = skuCounts[sku] + 1;
                }
                else
                {
                    skuCounts.Add(sku, 1);
                }
            }
        }

        private static int CalculateDiscountedPrice(char productId, int count)
        {
            int discount = 0;
            foreach (char sku in skus)
            {
                if (SpecialOffers.ContainsKey(sku))
            }
            return discount;
        }

        private static int CalculateTotalPrice()
        {
            int totalPrice = 0;
            foreach (var skuCount in skuCounts)
            {
                var product = Products.FirstOrDefault(x => x.Id == skuCount.Key);
                if (product != null)
                {
                    if (SpecialOffers.ContainsKey(skuCount.Key))
                    {
                        totalPrice += CalculateDiscountedPrice(skuCount.Key, skuCount.Value);
                    }
                    totalPrice += product.Price * skuCount.Value;
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

