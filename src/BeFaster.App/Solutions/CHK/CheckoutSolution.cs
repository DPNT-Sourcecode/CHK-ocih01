using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static readonly IList<Product> Products = new List<Product>();
        public static readonly IDictionary<char, IList<ISpecialOffer>> SpecialOffers = new Dictionary<char, IList<ISpecialOffer>>();
        
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
            skuCounts = HandleBuyOneProductGetAnotherProductFreeOffer(skuCounts);
            return skuCounts;
        }

        private static IDictionary<char, int> HandleBuyOneProductGetAnotherProductFreeOffer(IDictionary<char, int> skuCounts)
        {
            var specialOffers = SpecialOffers.Where(x => x.Value.Any(y=> y.GetType().Equals(typeof(BuyOneGetAnotherFree)))).ToDictionary(s => s.Key, s => s.Value);
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
                        GetDiscountedPrice(skuCount.Key, skuCount.Value, product.Price) : product.Price * skuCount.Value;
                }
                else
                {
                    return invalidInput;
                }
            }
            return totalPrice;
        }

        private static int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice)
        {
            int discountedPrice = 0;
            var specialOffers = SpecialOffers[productId];
            bool isFirstPriceCalculation = true;

            foreach(var offer in specialOffers)
            {
                if (offer is BuyMultipleForPriceReduction multiplePriceReductionOffer)
                {
                    int calculatedDiscountedPrice = multiplePriceReductionOffer.GetDiscountedPrice(productId, cartItemQuantity, actualProductPrice);
                    discountedPrice = isFirstPriceCalculation || calculatedDiscountedPrice < discountedPrice ? calculatedDiscountedPrice : discountedPrice;
                    isFirstPriceCalculation = false;
                }                
            }
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
            SpecialOffers.Add('A', new List<ISpecialOffer> {
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                },
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                }
            });
            SpecialOffers.Add('B', new List<ISpecialOffer>{
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 2,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                } });

            SpecialOffers.Add('E', new List<ISpecialOffer>{
                new BuyOneGetAnotherFree
                {
                    ItemQuantity = 2,
                    FreeItemId = 'B',
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree
                }
            });
        }
    }
}


