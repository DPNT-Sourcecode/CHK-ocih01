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
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'B',
                    ItemQuantity = 2,
                    SpecialPrice = 45
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'H',
                    ItemQuantity = 5,
                    SpecialPrice = 45
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'H',
                    ItemQuantity = 10,
                    SpecialPrice = 80
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'K',
                    ItemQuantity = 2,
                    SpecialPrice = 120
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'P',
                    ItemQuantity = 5,
                    SpecialPrice = 200
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'Q',
                    ItemQuantity = 3,
                    SpecialPrice = 80
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'V',
                    ItemQuantity = 2,
                    SpecialPrice = 90
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'V',
                    ItemQuantity = 3,
                    SpecialPrice = 130
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'E',
                    FreeItemId = 'B',
                    ItemQuantity = 2,
                    FreeItemQuantity = 1
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'F',
                    FreeItemId = 'F',
                    ItemQuantity = 2,
                    FreeItemQuantity = 1
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'N',
                    FreeItemId = 'M',
                    ItemQuantity = 3,
                    FreeItemQuantity = 1
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'R',
                    FreeItemId = 'Q',
                    ItemQuantity = 3,
                    FreeItemQuantity = 1
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'U',
                    FreeItemId = 'U',
                    ItemQuantity = 3,
                    FreeItemQuantity = 1
                }
            };
        }
    }
}


