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
        private static IDictionary<char, Product> products = GetProducts();

        private static IDictionary<char, Product> GetProducts()
        {
            var products = new Dictionary<char, Product>();
            List<Product> productList = new List<Product>();

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
            products = productList.ToDictionary(x => x.Id, x => x);
            return products;
        }
    }
}
