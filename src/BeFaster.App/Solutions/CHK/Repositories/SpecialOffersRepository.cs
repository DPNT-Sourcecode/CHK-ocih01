using System.Collections.Generic;
using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;

namespace BeFaster.App.Solutions.CHK.Repositories
{
    public class SpecialOffersRepository : ISpecialOffersRepository
    {
        public IList<ISpecialOffer> GetAllSpecialOffers()
        {
            return new List<ISpecialOffer>
            {
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 3,
                    SpecialPrice = 130
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 5,
                    SpecialPrice = 200
                }
            };
        }
    }
}

