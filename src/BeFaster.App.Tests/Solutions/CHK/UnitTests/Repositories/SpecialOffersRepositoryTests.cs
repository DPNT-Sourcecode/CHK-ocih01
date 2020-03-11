using BeFaster.App.Solutions.CHK.Models;
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

            Assert.AreEqual(16, offers.Count);
        }

        [TestMethod]
        public void GetSpecialOffersByType_Should_Return_Correct_Number_Of_BuyMultipleOfSameForPriceReductionOffers()
        {
            var offersCount = new SpecialOffersRepository().GetSpecialOffersByType<BuyMultipleOfSameForPriceReductionOffer>().Count();

            Assert.AreEqual(10, offersCount);
        }

        [TestMethod]
        public void GetSpecialOffersByType_Should_Return_Correct_Number_Of_BuyOneGetAnotherFreeOffers()
        {
            var offersCount = new SpecialOffersRepository().GetSpecialOffersByType<BuyOneGetAnotherFreeOffer>().Count();

            Assert.AreEqual(5, offersCount);
        }

        [TestMethod]
        public void GetSpecialOffersByType_Should_Return_Correct_Number_Of_BuyInBulkFromAGroupForPriceReductionOffers()
        {
            var offersCount = new SpecialOffersRepository().GetSpecialOffersByType<BuyInBulkFromAGroupForPriceReductionOffer>().Count();

            Assert.AreEqual(1, offersCount);
        }
    }
}


