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
            if (skus == null) { return invalidInput; }

            if (skus.Trim() == string.Empty) { return 0; }

            AddProducts();
            AddSpecialOffers();


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

        private static void AddProducts()
        {
            Products.Clear();
            Products.Add(new Product
            {
                Id = 'A',
                Price = 50
            });
            Products.Add(new Product
            {
                Id = 'B',
                Price = 30
            });
            Products.Add(new Product
            {
                Id = 'C',
                Price = 20
            });
            Products.Add(new Product
            {
                Id = 'D',
                Price = 15
            });
            Products.Add(new Product
            {
                Id = 'E',
                Price = 40
            });
        }

        private static void AddSpecialOffers()
        {
            SpecialOffers.Clear();
            SpecialOffers.Add('A', new SpecialOffer
            {
                ItemQuantity = 3,
                SpecialPrice = 130
            });
            SpecialOffers.Add('B', new SpecialOffer
            {
                ItemQuantity = 2,
                SpecialPrice = 45
            });
            SpecialOffers.Add('A', new SpecialOffer
            {
                ItemQuantity = 5,
                SpecialPrice = 200
            });
        }
    }
}

