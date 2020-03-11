using BeFaster.App.Solutions.CHK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeFaster.App.Tests.Solutions.CHK.E2ETests
{
    [TestClass]
    public class SpecialOfferTests
    {
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

        #region Special Offer Challenge 4

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_H_Given_MultipleValues()
        {
            Assert.AreEqual(45, CheckoutSolution.ComputePrice("HHHHH"));
            Assert.AreEqual(80, CheckoutSolution.ComputePrice("HHHHHHHHHH"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_K_Given_MultipleValues()
        {
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("KK"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_P_Given_MultipleValues()
        {
            Assert.AreEqual(200, CheckoutSolution.ComputePrice("PPPPP"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_Q_Given_MultipleValues()
        {
            Assert.AreEqual(80, CheckoutSolution.ComputePrice("QQQ"));
        }
        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_V_Given_MultipleValues()
        {
            Assert.AreEqual(90, CheckoutSolution.ComputePrice("VV"));
            Assert.AreEqual(130, CheckoutSolution.ComputePrice("VVV"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_N_Given_MultipleValues_Combined_With_M()
        {
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("NNNM"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_R_Given_MultipleValues_Combined_With_Q()
        {
            Assert.AreEqual(150, CheckoutSolution.ComputePrice("RRRQ"));
        }

        [TestMethod]
        public void ComputePrice_Should_Return_CorrectPrice_For_Product_U_Given_MultipleValues()
        {
            Assert.AreEqual(120, CheckoutSolution.ComputePrice("UUUU"));
        }

        #endregion

        #region Special Offer Challenge 5

        #endregion
    }
}
