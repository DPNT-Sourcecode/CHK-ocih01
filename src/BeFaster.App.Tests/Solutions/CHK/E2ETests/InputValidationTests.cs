using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.CHK.E2ETests
{
    [TestClass]
    public class InputValidationTests
    {
        const int invalidInput = -1;

        #region Invalid input tests

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_Empty_SKU_string()
        {
            Assert.AreEqual(0, CheckoutSolution.ComputePrice(string.Empty));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_Null_SKU_string()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice(null));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_WhiteSpace_As_SKU_string()
        {
            Assert.AreEqual(0, CheckoutSolution.ComputePrice("  "));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_SKU_That_Does_Not_Exist_In_ProductsList()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("1"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_SKU_That_Does_Not_Exist_In_ProductsList_With_Other_Valid_SKUs()
        {
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("A1C"));
        }

        #endregion
    }
}
