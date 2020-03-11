using System.Collections.Generic;
using BeFaster.App.Solutions.CHK.Interfaces;
using BeFaster.App.Solutions.CHK.Models;

namespace BeFaster.App.Solutions.CHK.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        public IDictionary<char, Product> GetProducts()
        {
            IDictionary<char, Product> products = new Dictionary<char, Product>(26);
            IList<int> priceList = GetPriceList();
            int i = 0;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                products.Add(c, new Product
                {
                    Id = c,
                    Price = priceList[i]

                });
                i++;
            }
            return products;
        }


        private IList<int> GetPriceList() => new List<int>{
            50, 30, 20, 15, 40, 10, 20, 10, 35, 60, 70, 90, 15, 40, 10, 50, 30,50, 20, 20, 40, 50, 20, 17, 20, 21
        };
    }
}


