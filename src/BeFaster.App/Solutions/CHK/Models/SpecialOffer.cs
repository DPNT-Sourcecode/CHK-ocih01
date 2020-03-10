using BeFaster.App.Solutions.CHK.Enums;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class SpecialOffer
    {
        public int ItemQuantity { get; set; }
        public SpecialOfferType OfferType{ get; set; }
    }

    public class BuyOneGetAnotherFree : SpecialOffer
    {
        public char FreeItemId { get; set; }
    }

    public class BuyMultipleForPriceReduction : SpecialOffer
    {
        public int SpecialPrice { get; set; }
    }
}