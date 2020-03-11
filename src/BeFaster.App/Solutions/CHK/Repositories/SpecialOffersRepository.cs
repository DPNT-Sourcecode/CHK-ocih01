using System.Collections.Generic;
using System.Linq;
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
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'A',
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'B',
                    ItemQuantity = 2,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'H',
                    ItemQuantity = 5,
                    SpecialPrice = 45,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'H',
                    ItemQuantity = 10,
                    SpecialPrice = 80,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'K',
                    ItemQuantity = 2,
                    SpecialPrice = 120,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'P',
                    ItemQuantity = 5,
                    SpecialPrice = 200,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'Q',
                    ItemQuantity = 3,
                    SpecialPrice = 80,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'V',
                    ItemQuantity = 2,
                    SpecialPrice = 90,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'V',
                    ItemQuantity = 3,
                    SpecialPrice = 130,
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction
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
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction,
                    OfferId=1
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'T',
                    ItemQuantity = 3,
                    SpecialPrice = 45,
                    IsGroupingAllowed = true,
                    CombinationProducts = new List<char>
                    {
                        'S', 'T', 'X', 'Y', 'Z'
                    },
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction,
                    OfferId=1
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'X',
                    ItemQuantity = 3,
                    SpecialPrice = 45,
                    IsGroupingAllowed = true,
                    CombinationProducts = new List<char>
                    {
                        'S', 'T', 'X', 'Y', 'Z'
                    },
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction,
                    OfferId=1
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'Y',
                    ItemQuantity = 3,
                    SpecialPrice = 45,
                    IsGroupingAllowed = true,
                    CombinationProducts = new List<char>
                    {
                        'S', 'T', 'X', 'Y', 'Z'
                    },
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction,
                    OfferId=1
                },
                new BuyMultipleProductsForPriceReductionOffer
                {
                    ProductId = 'Z',
                    ItemQuantity = 3,
                    SpecialPrice = 45,
                    IsGroupingAllowed = true,
                    CombinationProducts = new List<char>
                    {
                        'S', 'T', 'X', 'Y', 'Z'
                    },
                    OfferType = Enums.SpecialOfferType.BuyMultipleOfSameForPriceReduction,
                    OfferId=1
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
                }
            };
        }

        public IEnumerable<T> GetSpecialOffersByType<T>() where T : class
        {
            return GetAllSpecialOffers().OfType<T>();
        }
    }
}
