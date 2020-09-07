using System;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Exceptions;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Interfaces.Services;
using EcommerceSample.Interfaces.Util;
using EcommerceSample.Util;

namespace EcommerceSample.Services
{
    public class ProductService : IProductService
    {
        private ILogger _logger = LoggerFactory.CreateLogger();

        IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Result Add(Product product)
        {
            IsProductValid(product);

            var result = new Result();

            try
            {
                result = _productRepository.Add(product);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

            return result;
        }

        public Product GetByID(int id)
        {
            var product = _productRepository.GetByID(id);

            if (product == null)
                throw new ProductInvalidException("Product not found");

            return product;
        }

        private void IsProductValid(Product product)
        {
            if (product == null)
                throw new ProductInvalidException("Product can not be null");

            if (string.IsNullOrEmpty(product.Title))
                throw new ProductInvalidException("Title is not valid");

            if (product.Price < 0)
                throw new ProductInvalidException("Price is not valid");
        }
    }
}
