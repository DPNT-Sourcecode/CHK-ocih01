using BeFaster.App.Solutions.CHK.Models;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK.Interfaces
{
    public interface IProductsRepository
    {
        IDictionary<char, Product> GetAllProducts();
    }
}

