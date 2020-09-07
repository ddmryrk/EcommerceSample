using Xunit;
using Moq;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Entities;
using EcommerceSample.Services;
using EcommerceSample.Constants;
using EcommerceSample.Exceptions;

namespace EcommerceSampleTests.Services
{
    public class CategoryServiceTest
    {
        public CategoryServiceTest()
        {
        }

        [Fact]
        public void WhenAddCategory_AddedSuccesfully()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock
                .Setup(p => p.Add(It.IsAny<Category>()))
                .Returns(new Result(true));

            var category = new Category("Food");

            var categoryService = new CategoryService(categoryRepositoryMock.Object);
            var result = categoryService.Add(category);

            Assert.True(result.IsSucceed);
        }

        [Fact]
        public void WhenAddNullCategory_ExpectException()
        {
            var expectedErrorMessage = "Category cannot be null";
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock
                .Setup(p => p.Add(It.Is<Category>(data => data == null)))
                .Throws(new CategoryInvalidException(expectedErrorMessage));

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var ex = Assert.Throws<CategoryInvalidException>(() => categoryService.Add(null));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddCategoryTitleIsEmpty_ExpectException()
        {
            var expectedErrorMessage = "Title is not valid";
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock
                .Setup(p => p.Add(It.Is<Category>(data => data.Title == string.Empty)))
                .Throws(new CategoryInvalidException(expectedErrorMessage));

            var category = new Category("");

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var ex = Assert.Throws<CategoryInvalidException>(() => categoryService.Add(category));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }
    }
}
