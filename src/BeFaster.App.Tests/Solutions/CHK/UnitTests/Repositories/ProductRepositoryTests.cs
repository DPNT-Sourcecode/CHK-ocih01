using BeFaster.App.Solutions.CHK.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.CHK.UnitTests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        [TestMethod]
        public void GetAllProducts_Should_Return_Correct_Number_Of_Products()
        {
            var products = new ProductsRepository().GetAllProducts();

            Assert.AreEqual(26, products.Count);
        }

        [TestMethod]
        public void GetAllProducts_Should_Return_Correct_Number_Of_Product_Keys()
        {
            var products = new ProductsRepository().GetAllProducts();

            Assert.AreEqual(26, products.Count);
        }
    }
}

