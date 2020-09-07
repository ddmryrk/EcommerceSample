using EcommerceSample.Entities;
using EcommerceSample.Repositories;
using Xunit;

namespace EcommerceSampleTests.Repositories
{
    public class CategoryRepositoryTest
    {
        public CategoryRepositoryTest()
        {
           
        }

        [Fact]
        public void WhenAddCategory_AddedSuccessfully()
        {
            var categoryRepository = new CategoryRepository();
            var category = new Category("IPhone 3");

            var expectedResult = categoryRepository.GetAll().Count + 1;

            var result = categoryRepository.Add(category);
            var lastCount = categoryRepository.GetAll().Count;

            Assert.True(result.IsSucceed);
            Assert.Equal(expectedResult, lastCount);
        }

        [Fact]
        public void WhenGetCategory_GetSuccessfully()
        {
            var categoryRepository = new CategoryRepository();
            var category = new Category("IPhone 3");
            categoryRepository.Add(category);

            var selectedCategory = categoryRepository.GetByID(0);

            Assert.NotNull(selectedCategory);
        }
    }
}
