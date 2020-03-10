using BeFaster.App.Solutions.SUM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.SUM
{
    [TestClass]
    public class SumSolutionTest
    {
        [TestMethod]
        [DataRow(1, 1)]
        public void ComputeSum(int x, int y)
        {
            Assert.AreEqual(SumSolution.Sum(x, y), 2);
        }
    }
}
