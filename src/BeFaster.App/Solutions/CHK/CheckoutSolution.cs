﻿using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using BeFaster.App.Solutions.CHK.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static readonly IList<Product> Products = new List<Product>();
        public static readonly IDictionary<char, IList<ISpecialOffer>> SpecialOffers = new Dictionary<char, IList<ISpecialOffer>>();
        public static readonly ISpecialOfferService specialOfferService = new SpecialOfferService(SpecialOffers);
        private const int invalidInput = -1;

        public static int ComputePrice(string skus)
        {
            if (skus == null) { return invalidInput; }

            if (skus.Trim() == string.Empty) { return 0; }
            
            AddProducts();
            AddSpecialOffers();

            IDictionary<char, int> skuCounts = GetSkuCounts(skus);

            return GetTotalPrice(skuCounts);
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
            skuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);
            return skuCounts;
        }

        private static int GetTotalPrice(IDictionary<char, int> skuCounts)
        {
            int totalPrice = 0;

            //Filter Offers related to BuyMultipleForPriceReduction as we have already handled other offers
            var offers = SpecialOffers.Where(x => x.Value.Any(y => y.GetType().Equals(typeof(BuyMultipleForPriceReduction))))
               .ToDictionary(s => s.Key, s => s.Value.Where(z => z.OfferType == Enums.SpecialOfferType.BuyMultipleForPriceReduction).ToList());

            foreach (var skuCount in skuCounts)
            {
                var product = Products.FirstOrDefault(x => x.Id == skuCount.Key);
                if (product != null)
                {
                    totalPrice += offers.ContainsKey(skuCount.Key) ?
                        specialOfferService.GetDiscountedPrice(skuCount.Key, skuCount.Value, product.Price) : product.Price * skuCount.Value;
                }
                else
                {
                    return invalidInput;
                }
            }
            return totalPrice;
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
            Products.Add(new Product
            {
                Id = 'F',
                Price = 10
            });
            Products.Add(new Product
            {
                Id = 'G',
                Price = 20
            });
            Products.Add(new Product
            {
                Id = 'H',
                Price = 10
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
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree,
                    FreeItemQuantity = 1
                }
            });
            SpecialOffers.Add('N', new List<ISpecialOffer>{
                new BuyOneGetAnotherFree
                {
                    ItemQuantity = 3,
                    FreeItemId = 'M',
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree,
                    FreeItemQuantity = 1
                }
            });
            SpecialOffers.Add('F', new List<ISpecialOffer>{
                new BuyOneGetAnotherFree
                {
                    ItemQuantity = 2,
                    FreeItemId = 'F',
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree,
                    FreeItemQuantity = 1
                }
            });
            SpecialOffers.Add('H', new List<ISpecialOffer> {
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 5,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                },
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 10,
                    SpecialPrice = 80,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                }
            });
            SpecialOffers.Add('K', new List<ISpecialOffer>{
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 2,
                    SpecialPrice = 150,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                } });
           SpecialOffers.Add('P', new List<ISpecialOffer>{
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                } });
            SpecialOffers.Add('Q', new List<ISpecialOffer>{
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 3,
                    SpecialPrice = 80,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                } });
            SpecialOffers.Add('V', new List<ISpecialOffer>{
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 2,
                    SpecialPrice = 90,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                },
                new BuyMultipleForPriceReduction
                {
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = Enums.SpecialOfferType.BuyMultipleForPriceReduction
                } }
                );
        }
    }
}
