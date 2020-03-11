using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Interfaces
{
    public interface ISpecialOffersRepository
    {
        IList<ISpecialOffer> GetAllSpecialOffers();
    }
}

