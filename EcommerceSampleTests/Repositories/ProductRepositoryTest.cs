using EcommerceSample.Entities;
using EcommerceSample.Repositories;
using Xunit;


namespace EcommerceSampleTests.Repositories
{
    public class ProductRepositoryTest
    {
        public ProductRepositoryTest()
        {
        }

        [Fact]
        public void WhenAddProduct_AddedSuccesfully()
        {
            var phoneCategory = new Category("Phone");
            var product1 = new Product("İphone S5", 1000, phoneCategory);
            var repository = new ProductRepository();

            var result = repository.Add(product1);
            var count = repository.GetAll().Count;

            Assert.True(result.IsSucceed);
            Assert.Equal(1, count);
        }

        [Fact]
        public void WhenAddMultipleProduct_AddedSuccesfully()
        {
            var phoneCategory = new Category("Phone");
            var product1 = new Product("İphone S5", 1000, phoneCategory);
            var product2 = new Product("İphone 6", 1500, phoneCategory);
            var repository = new ProductRepository();

            var result1 = repository.Add(product1);
            var result2 = repository.Add(product2);

            Assert.True(result1.IsSucceed);
            Assert.True(result2.IsSucceed);
            Assert.Equal(2, repository.GetAll().Count);
        }

        [Fact]
        public void WhenAddProduct_IDControlSuccessful()
        {
            var phoneCategory = new Category("Phone");
            var product1 = new Product("İphone S5", 1000, phoneCategory);
            var repository = new ProductRepository();
            repository.Add(product1);

            var selectedProduct = repository.GetByID(0);

            Assert.NotNull(selectedProduct);
        }

        [Fact]
        public void WhenAddProductIdNotFound_ExpectNull()
        {
            var phoneCategory = new Category("Phone");
            var product1 = new Product("İphone S5", 1000, phoneCategory);
            var repository = new ProductRepository();
            repository.Add(product1);

            var selectedProduct = repository.GetByID(20);

            Assert.Null(selectedProduct);
        }       
    }
}
