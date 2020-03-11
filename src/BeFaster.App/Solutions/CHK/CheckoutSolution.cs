﻿using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Repositories;
using BeFaster.App.Solutions.CHK.Services;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        private static readonly ISpecialOfferService specialOfferService = new SpecialOfferService();
        private static readonly IProductsRepository productsRepository = new ProductsRepository();

        private static readonly ISpecialOffersRepository specialOffersRepository = new SpecialOffersRepository();

        private const int invalidInput = -1;

        public static int ComputePrice(string skus)
        {
            if (skus == null) { return invalidInput; }

            if (skus.Trim() == string.Empty) { return 0; }

            IDictionary<char, int> skuCounts = GetSkuCounts(skus);

            return GetTotalPrice(skuCounts);
        }

        private static IDictionary<char, int> GetSkuCounts(string skus)
        {
            IDictionary<char, int> skuCounts = new Dictionary<char, int>();
            foreach (char sku in skus)
            {
                if (skuCounts.ContainsKey(sku))
                {
                    skuCounts[sku] = skuCounts[sku] + 1;
                }
                else
                {
                    skuCounts.Add(sku, 1);
                }
            }
            var buyOneGetAnotherFreeOffers = specialOffersRepository.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>();
            return specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts, buyOneGetAnotherFreeOffers);
        }

        private static int GetTotalPrice(IDictionary<char, int> skuCounts)
        {
            int totalPrice = 0;
            var products = productsRepository.GetAllProducts();

            foreach (var skuCount in skuCounts)
            {
                if (products.Keys.Contains(skuCount.Key))
                {
                    var product = products[skuCount.Key];
                    var offers = product.BuyMultipleForPriceReductionOffers;
                    
                    totalPrice += offers != null && offers.Any() ? 
                        specialOfferService.GetDiscountedPrice(skuCount.Key, skuCount.Value, product.Price, offers)
                        : product.Price * skuCount.Value;
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

