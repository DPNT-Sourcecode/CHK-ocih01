using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using BeFaster.App.Solutions.CHK.Repositories;
using BeFaster.App.Solutions.CHK.Services;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        private static readonly ISpecialOfferService specialOfferService = new SpecialOfferService( new SpecialOffersRepository());
        private static readonly IProductsRepository productsRepository = new ProductsRepository();
        private static IDictionary<char, Product> products;

        private const int invalidInput = -1;

        public static int ComputePrice(string skus)
        {
            if (skus == null) { return invalidInput; }

            if (skus.Trim() == string.Empty) { return 0; }

            products = productsRepository.GetAllProducts();

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
            skuCounts =  specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);
            return specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, products);
        }

        private static int GetTotalPrice(IDictionary<char, int> skuCounts)
        {
            int totalPrice = 0;            

            foreach (var skuCount in skuCounts)
            {
                if (!products.Keys.Contains(skuCount.Key))
                    return invalidInput;
                
                var product = products[skuCount.Key];
                totalPrice += specialOfferService.GetDiscountedPrice(skuCount.Key, skuCount.Value, product.Price);
            }
            return totalPrice;
        }
    }
}
