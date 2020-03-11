using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;
using BeFaster.App.Solutions.CHK.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BeFaster.App.Tests.Solutions.CHK.UnitTests.Services
{
    [TestClass]
    public class SpecialOfferServiceTests
    {
        Mock<ISpecialOffersRepository> mockSpecialOffersRepository = new Mock<ISpecialOffersRepository>();
        SpecialOfferService specialOfferService;

        public SpecialOfferServiceTests()
        {
            specialOfferService = new SpecialOfferService(mockSpecialOffersRepository.Object);
        }

        #region GetDiscountedPriceTests
        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_A_MultiBuyOffer()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 3, 50);

            Assert.AreEqual(130, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>(), Times.Once);
        }


        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_A_MultiBuyOffer_1()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 5, 50);

            Assert.AreEqual(200, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_LowestPrice_Given_A_Multiple_MultiBuyOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 9, 50);

            Assert.AreEqual(380, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_MultiBuyOffers_That_DoesNot_Match()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 1, 50);

            Assert.AreEqual(50, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_No_MultiBuyOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('C', 2, 50);

            Assert.AreEqual(100, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>(), Times.Once);
        }
        #endregion

        #region ApplyBuyOneProductGetAnotherProductFreeOffer tests

        [TestMethod]
        public void ApplyBuyOneProductGetAnotherProductFreeOffer_Should_Return_Correct_Counts_Given_No_BuyOneGetOneOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1);
            skuCounts.Add('A', 2);

            var actualSkuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);

            Assert.AreEqual(skuCounts, actualSkuCounts);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyOneProductGetAnotherProductFreeOffer_Should_Return_Correct_Counts_Given_BuyOneGetSameProductOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>()).Returns(GetBuyOneGetAnotherFreeOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1);
            skuCounts.Add('A', 6);

            var actualSkuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);

            Assert.AreEqual(5, actualSkuCounts['A']);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyOneProductGetAnotherProductFreeOffer_Should_Return_Correct_Counts_Given_BuyOneGetDifferentProductOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>()).Returns(GetBuyOneGetAnotherFreeOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1);
            skuCounts.Add('E', 2);
            skuCounts.Add('B', 2);

            var actualSkuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);

            Assert.AreEqual(1, actualSkuCounts['B']);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyOneProductGetAnotherProductFreeOffer_Should_Return_Correct_Counts_Given_One_Matching_SKU_Item()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>()).Returns(GetBuyOneGetAnotherFreeOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1);
            skuCounts.Add('E', 2);
            skuCounts.Add('B', 1);

            var actualSkuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);

            Assert.AreEqual(0, actualSkuCounts['B']);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyOneProductGetAnotherProductFreeOffer_Should_Return_Correct_Counts_Given_Multiple_Matching_SKU_Item()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>()).Returns(GetBuyOneGetAnotherFreeOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1);
            skuCounts.Add('E', 4);
            skuCounts.Add('B', 1);

            var actualSkuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);

            Assert.AreEqual(0, actualSkuCounts['B']);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyOneProductGetAnotherProductFreeOffer_Should_Return_Correct_Counts_Given_No_Matching_SKU_Item()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>()).Returns(GetBuyOneGetAnotherFreeOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1);
            skuCounts.Add('E', 2);

            var actualSkuCounts = specialOfferService.ApplyBuyOneProductGetAnotherProductFreeOffer(skuCounts);

            Assert.AreEqual(skuCounts, actualSkuCounts);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>(), Times.Once);
        }

        #endregion

        private static List<BuyMultipleOfSameForPriceReductionOffer> GetMultiBuyOffers()
        {
            return new List<BuyMultipleOfSameForPriceReductionOffer>
            {
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = App.Solutions.CHK.Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = App.Solutions.CHK.Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                }
            };
        }

        private static List<BuyOneGetAnotherFreeOffer> GetBuyOneGetAnotherFreeOffers()
        {
            return new List<BuyOneGetAnotherFreeOffer>
            {
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'E',
                    ItemQuantity = 2,
                    FreeItemQuantity = 1,
                    FreeItemId = 'B'
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 5,
                    FreeItemQuantity = 1,
                    FreeItemId = 'A'
                }
            };
        }
    }
}
