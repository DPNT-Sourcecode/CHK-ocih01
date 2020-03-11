using BeFaster.App.Solutions.CHK;
using BeFaster.App.Solutions.CHK.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeFaster.App.Tests.Solutions.CHK.AllProductsTests
{
    [TestClass]
    public class AllProductsTests
    {
        private static IList<Product> products = GetProducts();

        private static IList<Product> GetProducts()
        {
            return JsonConvert.DeserializeObject<List<Product>>(GetProductsAsJsonString());
        }

        //I could not use Json file directly for some reason, hence doing it this way
        private static string GetProductsAsJsonString()
        {
            return "[{\"Id\":\"A\",\"Price\":50,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":3,\"SpecialPrice\":130},{\"Type\":0,\"ItemQuantity\":5,\"SpecialPrice\":200}]},{\"Id\":\"B\",\"Price\":30,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":2,\"SpecialPrice\":45}]},{\"Id\":\"C\",\"Price\":20},{\"Id\":\"D\",\"Price\":15},{\"Id\":\"E\",\"Price\":40,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":2,\"FreeItemId\":\"B\",\"FreeItemQuantity\":1}]},{\"Id\":\"F\",\"Price\":10,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":2,\"FreeItemId\":\"F\",\"FreeItemQuantity\":1}]},{\"Id\":\"G\",\"Price\":20},{\"Id\":\"H\",\"Price\":10,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":5,\"SpecialPrice\":45},{\"Type\":0,\"ItemQuantity\":10,\"SpecialPrice\":80}]},{\"Id\":\"I\",\"Price\":35},{\"Id\":\"J\",\"Price\":60},{\"Id\":\"K\",\"Price\":80,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":2,\"SpecialPrice\":150}]},{\"Id\":\"L\",\"Price\":90},{\"Id\":\"M\",\"Price\":15},{\"Id\":\"N\",\"Price\":40,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":3,\"FreeItemId\":\"M\",\"FreeItemQuantity\":1}]},{\"Id\":\"O\",\"Price\":10},{\"Id\":\"P\",\"Price\":50,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":5,\"SpecialPrice\":200}]},{\"Id\":\"Q\",\"Price\":30,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":3,\"SpecialPrice\":80}]},{\"Id\":\"R\",\"Price\":50,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":3,\"FreeItemId\":\"Q\",\"FreeItemQuantity\":1}]},{\"Id\":\"S\",\"Price\":30},{\"Id\":\"T\",\"Price\":20},{\"Id\":\"U\",\"Price\":40,\"BuyOneGetAnotherFreeOffers\":[{\"Type\":1,\"ItemQuantity\":3,\"FreeItemId\":\"U\",\"FreeItemQuantity\":1}]},{\"Id\":\"V\",\"Price\":50,\"BuyMultipleForPriceReductionOffers\":[{\"Type\":0,\"ItemQuantity\":2,\"SpecialPrice\":90},{\"Type\":0,\"ItemQuantity\":3,\"SpecialPrice\":130}]},{\"Id\":\"W\",\"Price\":20},{\"Id\":\"X\",\"Price\":90},{\"Id\":\"Y\",\"Price\":10},{\"Id\":\"Z\",\"Price\":50}]";
        }

        [TestMethod]
        public void CheckAllProductPricesAreCorrect()
        {
            foreach (var product in products)
            {
                Assert.AreEqual(product.Price, CheckoutSolution.ComputePrice(product.Id.ToString()));
            }
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_H_Given_MultipleValues()
        {            
            Assert.AreEqual(45, CheckoutSolution.ComputePrice("HHHHH"));
            Assert.AreEqual(80, CheckoutSolution.ComputePrice("HHHHHHHHHH"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_K_Given_MultipleValues()
        {
            Assert.AreEqual(150, CheckoutSolution.ComputePrice("KK"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_P_Given_MultipleValues()
        {
            Assert.AreEqual(200, CheckoutSolution.ComputePrice("PPPPP"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_Q_Given_MultipleValues()
        {
            Assert.AreEqual(80, CheckoutSolution.ComputePrice("QQQ"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_V_Given_MultipleValues()
        {
            Assert.AreEqual(90, CheckoutSolution.ComputePrice("VV"));
            Assert.AreEqual(130, CheckoutSolution.ComputePrice("VVV"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_N_Given_MultipleValues_Combined_With_M()
        {
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("NNNM"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_R_Given_MultipleValues_Combined_With_Q()
        {
            Assert.AreEqual(150, CheckoutSolution.ComputePrice("RRRQ"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_U_Given_MultipleValues()
        {
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("UUUU"));
        }
    }
}


