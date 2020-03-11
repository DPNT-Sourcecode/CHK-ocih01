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

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_A_MultiBuyOffer()
        {
            List<BuyMultipleOfSameForPriceReductionOffer> multiBuyOffers = GetMultiBuyOffers();
            mockSpecialOffersRepository.Setup(x => x.GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>()).Returns(multiBuyOffers).Verifiable();
        }

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
    }
}


