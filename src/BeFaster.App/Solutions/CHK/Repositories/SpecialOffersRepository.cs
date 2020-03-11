using System.Collections.Generic;
using System.Linq;
using BeFaster.App.Solutions.CHK.Enums;
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
                    SpecialPrice = 130,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'B',
                    ItemQuantity = 2,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'H',
                    ItemQuantity = 5,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'H',
                    ItemQuantity = 10,
                    SpecialPrice = 80,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'K',
                    ItemQuantity = 2,
                    SpecialPrice = 120,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'P',
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'Q',
                    ItemQuantity = 3,
                    SpecialPrice = 80,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'V',
                    ItemQuantity = 2,
                    SpecialPrice = 90,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleOfSameForPriceReductionOffer
                {
                    ProductId = 'V',
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'E',
                    FreeItemId = 'B',
                    ItemQuantity = 2,
                    FreeItemQuantity = 1,
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'F',
                    FreeItemId = 'F',
                    ItemQuantity = 2,
                    FreeItemQuantity = 1,
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree
                },
                new BuyOneGetAnotherFreeOffer
                {
                    ProductId = 'N',
                    FreeItemId = 'M',
                    ItemQuantity = 3,
                    FreeItemQuantity = 1,
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree
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
                    FreeItemQuantity = 1,
                    OfferType = Enums.SpecialOfferType.BuyOneGetAnotherFree
                },
                new BuyInBulkFromAGroupForPriceReductionOffer
                {
                    Products = new List<char>
                    {
                        'S', 'T', 'X', 'Y', 'Z'
                    },
                    ItemQuantity = 3,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyInBulkFromAGroupForPriceReductionOffer
                }
            };
        }

        public IList<T> GetSpecialOffersByType<T>() where T : class
        {
            return GetAllSpecialOffers().OfType<T>().ToList();
        }
    }
}

