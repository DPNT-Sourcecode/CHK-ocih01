using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeFaster.App.Tests.Solutions.CHK.UnitTests.Services
{
    [TestClass]
    public class SpecialOfferServiceTests
    {
        Mock<ISpecialOffersRepository> mockSpecialOffersRepository = new Mock<ISpecialOffersRepository>();

        [TestMethod]
        public void GetDiscountedPrice_Should_Return_CorrectPrice_Given_A_MultiBuyOffer()
        {
            var specialOfferService = new SpecialOfferService(mockSpecialOffersRepository.Object);
        }
    }
}

