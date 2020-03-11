using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using BeFaster.App.Solutions.CHK.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        private static IDictionary<char, Product> products = GetProducts();
        private static readonly Dictionary<char, IList<BuyOneGetAnotherFreeOffer>> buyOneGetAnotherProductOffers = GetBuyOneGetAnotherProductOffersOffers();
        private static readonly ISpecialOfferService specialOfferService = new SpecialOfferService();

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
            return specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts, buyOneGetAnotherProductOffers);
        }

        private static int GetTotalPrice(IDictionary<char, int> skuCounts)
        {
            int totalPrice = 0;

            foreach (var skuCount in skuCounts)
            {                
                if (products.Keys.Contains(skuCount.Key))
                {
                    var product = products[skuCount.Key];
                    var offers = product.BuyMultipleForPriceReductionOffers;
                    totalPrice += offers != null && offers.Any() ? specialOfferService.GetDiscountedPrice(skuCount.Key, skuCount.Value, product.Price, offers) 
                        : product.Price * skuCount.Value;
                }
                else
                {
                    return invalidInput;
                }
            }
            return totalPrice;
        }

        private static IDictionary<char, Product> GetProducts()
        {
            var products = new Dictionary<char, Product>();
            List<Product> productList = new List<Product>();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Solutions\CHK\Data\Products.json");

            JsonSerializer serializer = new JsonSerializer();
            using (FileStream s = File.Open(filePath, FileMode.Open))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                while (!sr.EndOfStream)
                {
                    productList = serializer.Deserialize<List<Product>>(reader);
                }
            }
            products = productList.ToDictionary(x => x.Id, x => x);
            return products;
        }

        private static Dictionary<char, IList<BuyOneGetAnotherFreeOffer>> GetBuyOneGetAnotherProductOffersOffers()
        {
            return products.Where(x => x.Value.BuyOneGetAnotherFreeOffers != null).ToDictionary(s => s.Key, s => s.Value.BuyOneGetAnotherFreeOffers);
        }
    }
}

