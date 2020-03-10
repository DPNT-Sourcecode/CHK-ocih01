using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestClass]
    public class CheckOutSolutionTests
    {
        [TestMethod]
        public void ComputePrice_Should_Return_Zero_Given_No_SKU()
        {
            Assert.AreEqual(0, CheckoutSolution.ComputePrice(string.Empty));
        }
    }
}


