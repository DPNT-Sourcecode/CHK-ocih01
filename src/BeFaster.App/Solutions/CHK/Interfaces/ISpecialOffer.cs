using BeFaster.App.Solutions.CHK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Solutions.CHK.Interfaces
{
    public interface ISpecialOffer
    {
        int ItemQuantity { get; set; }
        SpecialOfferType OfferType { get; set; }
    }
}
