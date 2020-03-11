using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using BeFaster.App.Solutions.CHK.Repositories;
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
        private static readonly Dictionary<char, BuyOneGetAnotherFreeOffer> buyOneGetAnotherProductOffers = GetBuyOneGetAnotherProductOffersOffers();
        private static readonly ISpecialOfferService specialOfferService = new SpecialOfferService();
        private static readonly ISpecialOffers productsRepository = new ProductsRepository();
        private static readonly ISpecialOffers specialOffersRepository = new SpecialOffersRepository();

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

        private static IDictionary<char, Product> GetProducts()
        {
            List<Product> productList = new List<Product>();

            productList = JsonConvert.DeserializeObject<List<Product>>(GetProductsAsJsonString());

            return productList.ToDictionary(x => x.Id, x => x);
        }

        private static Dictionary<char, BuyOneGetAnotherFreeOffer> GetBuyOneGetAnotherProductOffersOffers()
        {
            return products.Where(x => x.Value.BuyOneGetAnotherFreeOffer != null).ToDictionary(s => s.Key, s => s.Value.BuyOneGetAnotherFreeOffer);
        }

        //I could not use Json file directly for some reason, hence doing it this way
        private static string GetProductsAsJsonString()
        {
            return "[{\"Id\":\"A\",\"Price\":50,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":3,\"SpecialPrice\":130}," +
                "{\"Type\":0,\"ItemQuantity\":5,\"SpecialPrice\":200}]}," +
                "{\"Id\":\"B\",\"Price\":30,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":2,\"SpecialPrice\":45}]}," +
                "{\"Id\":\"C\",\"Price\":20},{\"Id\":\"D\",\"Price\":15}," +
                "{\"Id\":\"E\",\"Price\":40,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":2,\"FreeItemId\":\"B\",\"FreeItemQuantity\":1}]}," +
                "{\"Id\":\"F\",\"Price\":10,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":2,\"FreeItemId\":\"F\",\"FreeItemQuantity\":1}]}," +
                "{\"Id\":\"G\",\"Price\":20},{\"Id\":\"H\",\"Price\":10,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":5,\"SpecialPrice\":45}," +
                "{\"Type\":0,\"ItemQuantity\":10,\"SpecialPrice\":80}]},{\"Id\":\"I\",\"Price\":35},{\"Id\":\"J\",\"Price\":60}," +
                "{\"Id\":\"K\",\"Price\":80,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":2,\"SpecialPrice\":150}]}," +
                "{\"Id\":\"L\",\"Price\":90}," +
                "{\"Id\":\"M\",\"Price\":15}," +
                "{\"Id\":\"N\",\"Price\":40,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":3,\"FreeItemId\":\"M\",\"FreeItemQuantity\":1}]}," +
                "{\"Id\":\"O\",\"Price\":10},{\"Id\":\"P\",\"Price\":50,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":5,\"SpecialPrice\":200}]}," +
                "{\"Id\":\"Q\",\"Price\":30,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":3,\"SpecialPrice\":80}]}," +
                "{\"Id\":\"R\",\"Price\":50,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":3,\"FreeItemId\":\"Q\",\"FreeItemQuantity\":1}]}," +
                "{\"Id\":\"S\",\"Price\":30},{\"Id\":\"T\",\"Price\":20},{\"Id\":\"U\",\"Price\":40,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":3,\"FreeItemId\":\"U\",\"FreeItemQuantity\":1}]},{\"Id\":\"V\",\"Price\":50,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":2,\"SpecialPrice\":90},{\"Type\":0,\"ItemQuantity\":3,\"SpecialPrice\":130}]},{\"Id\":\"W\",\"Price\":20},{\"Id\":\"X\",\"Price\":90},{\"Id\":\"Y\",\"Price\":10},{\"Id\":\"Z\",\"Price\":50}]";
        }
    }
}

