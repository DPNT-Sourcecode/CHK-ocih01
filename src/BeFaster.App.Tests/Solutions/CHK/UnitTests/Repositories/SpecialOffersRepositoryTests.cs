using BeFaster.App.Solutions.CHK.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BeFaster.App.Tests.Solutions.CHK.UnitTests.Repositories
{
    [TestClass]
    public class SpecialOffersRepositoryTests
    {
        [TestMethod]
        public void GetAllSpecialOffers_Should_Return_Correct_Number_Of_SpecialOffers()
        {
            var offers = new SpecialOffersRepository().GetAllSpecialOffers();

            Assert.AreEqual(26, offers.Count);
        }

        [TestMethod]
        public void GetAllProducts_Should_Return_Correct_Number_Of_Product_Keys()
        {
            var products = new ProductsRepository().GetAllProducts();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                Assert.IsTrue(products.Keys.Contains(c));
            }
        }
    }
}
