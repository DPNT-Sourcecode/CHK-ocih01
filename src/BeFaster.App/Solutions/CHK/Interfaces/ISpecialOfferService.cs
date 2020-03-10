using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Solutions.CHK.Interfaces
{
    public interface ISpecialOfferService
    {
        int GetDiscountedPrice(char productId, int cartItemQuantity, int actualProductPrice);
        IDictionary<char, int> ApplyBuyOneProductGetAnotherProductFreeOffer(IDictionary<char, int> skuCounts);
    }
}
