using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeFaster.App.Solutions.CHK.Models;

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
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU()
        {
            AddProducts();
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("A"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_Distinct_SKUs()
        {
            AddProducts();
            Assert.AreEqual(35, CheckoutSolution.ComputePrice("CD"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_Repeated_SKUs()
        {
            AddProducts();
            Assert.AreEqual(40, CheckoutSolution.ComputePrice("CC"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_Repeated_And_NonRepeated_SKUs()
        {
            AddProducts();
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("CDCDA"));
        }

        private void AddProducts()
        {
            CheckoutSolution.Products.Clear();
            CheckoutSolution.Products.Add(new Product
            {
                Id = 'A',
                Price = 50
            });
            CheckoutSolution.Products.Add(new Product
            {
                Id = 'B',
                Price = 30
            });
            CheckoutSolution.Products.Add(new Product
            {
                Id = 'C',
                Price = 20
            });
            CheckoutSolution.Products.Add(new Product
            {
                Id = 'D',
                Price = 15
            });
        }
    }
}





