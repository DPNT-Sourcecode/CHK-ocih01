using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestClass]
    public class CheckOutSolutionTests
    {
        const int invalidInput = -1;

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_Empty_SKU_string()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice(string.Empty));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_Null_SKU_string()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice(null));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_WhiteSpace_As_SKU_string()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("  "));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_Empty_ProductsList()
        {
            CheckoutSolution.Products.Clear();
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("Z"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_Does_Exist_In_ProductList()
        {
           

            Assert.AreEqual(10, CheckoutSolution.ComputePrice("A"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKUs_That_Does_Exist_In_ProductList()
        {
            CheckoutSolution.Products.Add(new App.Solutions.CHK.Models.Product
            {
                Id = 'A',
                Price = 10
            });

            Assert.AreEqual(10, CheckoutSolution.ComputePrice("A"));
        }

        private void AddProducts()
        {
            CheckoutSolution.Products = new List<Product>( new Product
            {
                Id = 'A',
                Price = 50
            },
            new Product
            {
                Id = 'B',
                Price = 30
            },
            new Product
            {
                Id = 'C',
                Price = 20
            },
            new Product
            {
                Id = 'D',
                Price = 15
            });
        }
    }
}


