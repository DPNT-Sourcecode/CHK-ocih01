using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        #region Special Offer Tests Challenge 1

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_With_SpecialOffer()
        {
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("A"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_Satisfies_SpecialOffer()
        {
            Assert.AreEqual(130, CheckoutSolution.ComputePrice("AAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_SpecialOffer()
        {
            Assert.AreEqual(180, CheckoutSolution.ComputePrice("AAAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_has_One_Combination_That_Satisfies_SpecialOffer()
        {
            Assert.AreEqual(150, CheckoutSolution.ComputePrice("ACAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_has_SingleCombination_That_Satisfies_SpecialOffer()
        {
            Assert.AreEqual(175, CheckoutSolution.ComputePrice("ABABA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_has_Multiple_Combination_That_Satisfies_SpecialOffer()
        {
            Assert.AreEqual(220, CheckoutSolution.ComputePrice("ABABABB"));
        }


        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_Satisfies_Single_SpecialOffer_Along_With_Regular_SKU()
        {
            Assert.AreEqual(215, CheckoutSolution.ComputePrice("ABABACC"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Multiple_SKU_That_Satisfies_Multiple_SpecialOffers_Along_With_Regular_SKU()
        {
            Assert.AreEqual(255, CheckoutSolution.ComputePrice("ABABABBCD"));
        }

        #endregion

        #region Special Offer Tests Challenge 2

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_Multiple_PriceReduction_SpecialOffers()
        {
            Assert.AreEqual(200, CheckoutSolution.ComputePrice("AAAAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Lowest_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_Multiple_PriceReduction_SpecialOffers()
        {
            Assert.AreEqual(330, CheckoutSolution.ComputePrice("AAAAAAAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Lowest_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_Multiple_PriceReduction_SpecialOffers_1()
        {
            Assert.AreEqual(380, CheckoutSolution.ComputePrice("AAAAAAAAA"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_Satisfies_Single_BuyOneGetAnotherFree_Offer()
        {
            Assert.AreEqual(80, CheckoutSolution.ComputePrice("EEB"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_Single_BuyOneGetAnotherFree_Offer()
        {
            Assert.AreEqual(110, CheckoutSolution.ComputePrice("EEBB"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_has_Combination_That_Satisfies_Single_BuyOneGetAnotherFree_Offer_WithOtherRegularOffers()
        {
            Assert.AreEqual(310, CheckoutSolution.ComputePrice("EEBAAAAAB"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_ThatDoesNot_Satisfy_Single_BuyOneGetAnotherFree_Offer_WithOtherRegularOffers()
        {
            Assert.AreEqual(285, CheckoutSolution.ComputePrice("EBAAAAAB"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_ThatHas_Multiple_Combinations_That_Satisfy_MultipleOffers_WithOtherRegularOffers()
        {
            Assert.AreEqual(455, CheckoutSolution.ComputePrice("AAAAAEEBAAABB"));
        }

        #endregion

        #region Special Offer Tests Challenge 3
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_Satisfies_Single_BuyOneGetSameFree_Offer()
        {
            Assert.AreEqual(30, CheckoutSolution.ComputePrice("FFFF"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_That_Satisfies_Multiple_BuyOneGetSameFree_Offer()
        {
            Assert.AreEqual(50, CheckoutSolution.ComputePrice("FFFFFFF"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_ThatDoesNot_Satisfy_BuyOneGetSameFree_Offer()
        {
            Assert.AreEqual(10, CheckoutSolution.ComputePrice("F"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_Single_SKU_ThatDoes_Satisfy_Single_BuyOneGetSameFree_Offer_WithNoFreeItems()
        {
            Assert.AreEqual(20, CheckoutSolution.ComputePrice("FF"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_Correct_TotalPrice_Given_MultipleSKU_ThatDoes_Satisfy_Single_BuyOneGetSameFree_Offer_WithBalancedFreeItems()
        {
            Assert.AreEqual(40, CheckoutSolution.ComputePrice("FFFFFF"));
        }

        #endregion
    }
}
