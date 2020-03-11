using BeFaster.App.Solutions.CHK;
using BeFaster.App.Solutions.CHK.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeFaster.App.Tests.Solutions.CHK.AllProductsTests
{
    [TestClass]
    public class AllProductsTests
    {
        private static IList<Product> products = GetProducts();

        private static IList<Product> GetProducts()
        {
            IList<Product> productList = new List<Product>();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Solutions\CHK\TestData\Products.json");

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
            return productList;
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
    }
}
