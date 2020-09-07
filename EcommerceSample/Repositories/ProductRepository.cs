using System.Collections.Generic;
using System.Linq;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Util;

namespace EcommerceSample.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>();
        }

        public Result Add(Product product)
        {
            var result = new Result();

            product.ID = Helper.GenerateIDByList(_products);
            _products.Add(product);
            result.IsSucceed = false;
         
            return result;
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product GetByID(int id)
        {
            return _products.Where(p => p.ID == id).FirstOrDefault();
        }

        
    }
}
