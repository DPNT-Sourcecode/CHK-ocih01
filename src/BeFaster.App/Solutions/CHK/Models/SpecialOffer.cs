using BeFaster.App.Solutions.CHK.Enums;
using BeFaster.App.Solutions.CHK.Interfaces;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class SpecialOffer : ISpecialOffer
    {
        public int ItemQuantity { get; set; }
        public SpecialOfferType OfferType{ get; set; }
    }
}