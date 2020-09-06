using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Services;
using Xunit;
using Moq;
using EcommerceSample.Exceptions;
using EcommerceSample.Interfaces.Repositories;

namespace UnitTest
{
    public class ProductServiceTest
    {
        [Fact]
        public void WhenAddProduct_AddedSuccesfully()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(p => p.Add(It.IsAny<Product>()))
                .Returns(new Result(true));

            var category = new Category("Food");
            var product = new Product("Apple", 1.00m, category);
            var expectedResult = true;
            
            var productService = new ProductService(productRepositoryMock.Object);
            var result = productService.Add(product);

            Assert.Equal(expectedResult, result.IsSucceed);
        }

        [Fact]
        public void WhenAddNullProduct_ExpectException()
        {
            var expectedErrorMessage = "Product can not be null";
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(p => p.Add(It.Is<Product>(data => data == null)))
                .Throws(new ProductInvalidException(expectedErrorMessage));

            var productService = new ProductService(productRepositoryMock.Object);

            var ex = Assert.Throws<ProductInvalidException>(() => productService.Add(null));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddProductTitleIsEmpty_ExpectException()
        {
            var expectedErrorMessage = "Title is not valid";
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(p => p.Add(It.Is<Product>(data => data.Title == string.Empty)))
                .Throws(new ProductInvalidException(expectedErrorMessage));

            var category = new Category("Food");
            var product = new Product(string.Empty, 1.00m, category);

            var productService = new ProductService(productRepositoryMock.Object);

            var ex = Assert.Throws<ProductInvalidException>(() => productService.Add(product));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddProductPriceIsMinus_ExpectException()
        {
            var expectedErrorMessage = "Price is not valid";
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(p => p.Add(It.Is<Product>(data => data.Price < 0)))
                .Throws(new ProductInvalidException(expectedErrorMessage));

            var category = new Category("Food");
            var product = new Product("Apple", -1.00m, category);

            var productService = new ProductService(productRepositoryMock.Object);

            var ex = Assert.Throws<ProductInvalidException>(() => productService.Add(product));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenGetProduct_GetSuccesfully()
        {
            var category = new Category("Food");
            var product = new Product("Apple", 1.00m, category); 

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(p => p.GetByID(It.IsAny<int>()))
                .Returns(product);            

            var productService = new ProductService(productRepositoryMock.Object);
            var result = productService.GetByID(1);

            Assert.True(result != null);
        }

        [Fact]
        public void WhenGetProductWrongID_ExpectException()
        {
            var expectedErrorMessage = "Product not found";

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(p => p.GetByID(It.IsAny<int>()))
                .Returns((Product)null);

            var productService = new ProductService(productRepositoryMock.Object);

            var ex = Assert.Throws<ProductInvalidException>(() => productService.GetByID(1));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }
    }
}
