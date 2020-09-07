using System.Collections.Generic;
using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Result Add(Product product);
        Product GetByID(int id);
        List<Product> GetAll();
    }
}
