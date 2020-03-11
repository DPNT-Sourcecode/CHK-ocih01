using BeFaster.App.Solutions.CHK.Interfaces;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Models
{
    public class Product
    {
        public char Id { get; set; }
        public int Price { get; set; }
        public IList<SpecialOffer> SpecialOffers{get; set;}
    }
}
