using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void ComputePrice_Should_Return_InvalidInput_Given_Product_That_Does_NotExist_In_ProductsList()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("Z"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_SKU_That_Does_Exist_In_ProductList()
        {
            CheckoutSolution.Products.Add(new App.Solutions.CHK.Models.Product
            {
                Id = "A",
                Price = 10
            });

            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("A"));
        }
    }
}




