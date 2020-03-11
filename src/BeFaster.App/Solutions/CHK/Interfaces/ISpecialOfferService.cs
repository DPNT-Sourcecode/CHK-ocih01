using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Interfaces
{
    public interface ISpecialOfferService
    {
        int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice, IList<BuyMultipleForPriceReductionOffer> specialOffers);
        IDictionary<char, int> ApplyBuyOneProductGetAnotherProductFreeOffer(IDictionary<char, int> skuCounts, Dictionary<char, IList<BuyOneGetAnotherFreeOffer>> offers);
    }
}
