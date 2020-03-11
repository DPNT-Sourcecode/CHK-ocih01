﻿using BeFaster.App.Solutions.CHK.Enums;
using System;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Interfaces
{
    public interface ISpecialOffersRepository
    {
        IList<ISpecialOffer> GetAllSpecialOffers();
        IList<ISpecialOffer> GetSpecialOffersByType(SpecialOfferType specialOfferType );
    }
}


