using BeFaster.App.Solutions.TST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.TST
{
    [TestClass]
    public class OneTest {
    
        [TestMethod]
        public void RunApply() {
            Assert.AreEqual(One.apply(), 1);
        }
    }
}
