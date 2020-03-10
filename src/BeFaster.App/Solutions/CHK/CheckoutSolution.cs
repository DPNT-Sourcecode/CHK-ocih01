using BeFaster.App.Solutions.CHK.Models;
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

            IDictionary<char, int> skuCounts = GetSkuCounts(skus);

            return CalculateTotalPrice(skuCounts);
        }

        private static IDictionary<char, int> GetSkuCounts(string skus)
        {
            IDictionary<char, int> skuCounts = new Dictionary<char, int>();
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
            return skuCounts;
        }

        private static int CalculateTotalPrice(IDictionary<char, int> skuCounts)
        {
            int totalPrice = 0;
            foreach (var skuCount in skuCounts)
            {
                var product = Products.FirstOrDefault(x => x.Id == skuCount.Key);
                if (product != null)
                {
                    totalPrice += SpecialOffers.ContainsKey(skuCount.Key) ?
                        CalculateDiscountedPrice(skuCount.Key, skuCount.Value, product.Price) : product.Price * skuCount.Value;
                }
                else
                {
                    return invalidInput;
                }
            }
            return totalPrice;
        }

        private static int CalculateDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;
            var specialOffer = SpecialOffers[productId];

            discountedPrice = cartItemQuantity / specialOffer.ItemQuantity * specialOffer.SpecialPrice;
            discountedPrice += cartItemQuantity % specialOffer.ItemQuantity * actualProductPrice;

            return discountedPrice;
        }
    }
}

