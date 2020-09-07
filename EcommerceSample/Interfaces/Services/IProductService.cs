using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Services
{
    public interface IProductService
    {
        Result Add(Product product);
        Product GetByID(int id);
    }
}
