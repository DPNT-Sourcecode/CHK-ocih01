using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestClass]
    public class CheckOutSolutionTests
    {
        #region Simple Product Price Tests

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU()
        {
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("A"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_Distinct_SKUs()
        {
            Assert.AreEqual(35, CheckoutSolution.ComputePrice("CD"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_Repeated_SKUs()
        {
            Assert.AreEqual(40, CheckoutSolution.ComputePrice("CC"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_Repeated_And_NonRepeated_SKUs()
        {
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("CDCDA"));
        }

        #endregion
    }
}
