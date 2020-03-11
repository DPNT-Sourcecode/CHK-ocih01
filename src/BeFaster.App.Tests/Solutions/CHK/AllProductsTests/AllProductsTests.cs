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
    }
}


