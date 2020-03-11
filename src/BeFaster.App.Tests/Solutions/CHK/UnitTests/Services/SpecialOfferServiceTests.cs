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
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 3, 50);

            Assert.AreEqual(130, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }


        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_A_MultiBuyOffer_1()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 5, 50);

            Assert.AreEqual(200, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_LowestPrice_Given_A_Multiple_MultiBuyOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 9, 50);

            Assert.AreEqual(380, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_MultiBuyOffers_That_DoesNot_Match()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('A', 1, 50);

            Assert.AreEqual(50, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_No_MultiBuyOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Verifiable();

            var price = specialOfferService.GetDiscountedPrice('C', 2, 50);

            Assert.AreEqual(100, price);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
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

        #region ApplyBuyGroupOfProductsForPriceReductionOffer

        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_No_GroupBuyPriceReductionOffers()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'A', 2 }
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, null);

            Assert.AreEqual(skuCounts, actualSkuCounts);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_A_GroupBuyPriceReductionOffers_For_one_SKU()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'S', 3 }
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, GetProducts());

            Assert.AreEqual(skuCounts, actualSkuCounts);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_A_GroupBuyPriceReductionOffers_For_multiple_SKU()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'S', 3 },
                { 'Y', 3 }
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, GetProducts());

            Assert.AreEqual(skuCounts, actualSkuCounts);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_A_GroupBuyPriceReductionOffers_For_Combination_Of_HighAndLow_SKU()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'S', 3 },
                { 'Z', 4 }
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, GetProducts());

            Assert.AreEqual(3, actualSkuCounts['Z']);
            Assert.AreEqual(4, actualSkuCounts['S']);
            
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }


        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_A_GroupBuyPriceReductionOffers_For_Combination_Of_HighAndLow_SKU_1()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'S', 3 },
                { 'Z', 4 },
                { 'X', 2 }
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, GetProducts());

            Assert.AreEqual(0, actualSkuCounts['Z']);
            Assert.AreEqual(9, actualSkuCounts['S']);
            Assert.AreEqual(0, actualSkuCounts['X']);

            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_A_GroupBuyPriceReductionOffers_For_That_Does_Not_Match_InQuantity()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'S', 1 },
                { 'Z', 1 },
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, GetProducts());

            Assert.AreEqual(skuCounts, actualSkuCounts);
            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        [TestMethod]
        public void ApplyBuyGroupOfProductsForPriceReductionOffer_Should_Return_Correct_Counts_Given_A_GroupBuyPriceReductionOffers_For_Even_SKU_Counts()
        {
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>()).Returns(GetMultiBuyOffers()).Verifiable();

            var skuCounts = new Dictionary<char, int>(1)
            {
                { 'S', 2 },
                { 'Z', 2 },
                { 'X', 2 },
                { 'Y', 2 },
                { 'T', 2 },
            };

            var actualSkuCounts = specialOfferService.ApplyBuyGroupOfProductsForPriceReductionOffer(skuCounts, GetProducts());

            Assert.AreEqual(0, actualSkuCounts['Z']);
            Assert.AreEqual(0, actualSkuCounts['Y']);
            Assert.AreEqual(3, actualSkuCounts['S']);
            Assert.AreEqual(3, actualSkuCounts['T']);
            Assert.AreEqual(4, actualSkuCounts['X']);

            mockSpecialOffersRepository.Verify(x => x.GetSpecialOffersByType<BuyMultipleProductsForPriceReductionOffer>(), Times.Once);
        }

        #endregion

        private static IDictionary<char, Product> GetProducts()
        {
            return new Dictionary<char, Product>(1)
            {
                {
                    'S',
                    new Product
                    {
                        Price = 20
                    }
                },
                {
                    'X',
                    new Product
                    {
                        Price = 17
                    }
                },
                 {
                    'T',
                    new Product
                    {
                        Price = 20
                    }
                },
                 {
                    'Y',
                    new Product
                    {
                        Price = 20
                    }
                },
                 {
                    'Z',
                    new Product
                    {
                        Price = 21
                    }
                }
            };
        }

        private static List<BuyMultipleProductsForPriceReductionOffer> GetMultiBuyOffers()
        {
            return new List<BuyMultipleProductsForPriceReductionOffer>
            {
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = App.Solutions.CHK.Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = App.Solutions.CHK.Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'S',
                    ItemQuantity = 3,
                    SpecialPrice = 45,
                    IsGroupingAllowed = true,
                    CombinationProducts = new List<char>
                    {
                        'S', 'T', 'X', 'Y', 'Z'
                    },
                    OfferType = App.Solutions.CHK.Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction,
                    OfferId=1
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
