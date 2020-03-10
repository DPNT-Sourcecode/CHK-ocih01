using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeFaster.App.Solutions.CHK.Models;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestClass]
    public class CheckOutSolutionTests
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
        public void ComputePrice_Should_Return_InvalidInput_Given_Empty_ProductsList()
        {
            CheckoutSolution.Products.Clear();
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("Z"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_SKU_That_Does_Not_Exist_In_ProductsList()
        {
            AddProducts();
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("Z"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_InvalidInput_Given_SKU_That_Does_Not_Exist_In_ProductsList_With_Other_Valid_SKUs()
        {
            AddProducts();
            Assert.AreEqual(invalidInput, CheckoutSolution.ComputePrice("AZC"));
        }

        #endregion

        #region Products Only Tests

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU()
        {
            AddProducts();
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("A"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU()
        {
            AddProducts();
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("B"));
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

        #endregion

        #region Special Offer Only Tests

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_With_SpecialOffer()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("A"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_Satisfies_SpecialOffer()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(130, CheckoutSolution.ComputePrice("AAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_SpecialOffer()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(180, CheckoutSolution.ComputePrice("AAAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_has_One_Combination_That_Satisfies_SpecialOffer()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(150, CheckoutSolution.ComputePrice("ACAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_has_SingleCombination_That_Satisfies_SpecialOffer()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(175, CheckoutSolution.ComputePrice("ABABA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_has_Multiple_Combination_That_Satisfies_SpecialOffer()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(220, CheckoutSolution.ComputePrice("ABABABB"));
        }


        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_Satisfies_Single_SpecialOffer_Along_With_Regular_SKU()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(215, CheckoutSolution.ComputePrice("ABABACC"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_Satisfies_Multiple_SpecialOffers_Along_With_Regular_SKU()
        {
            AddProducts();
            AddSpecialOffers();
            Assert.AreEqual(255, CheckoutSolution.ComputePrice("ABABABBCD"));
        }

        #endregion
    }
}




