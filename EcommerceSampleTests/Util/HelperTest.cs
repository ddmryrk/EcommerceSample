using System.Collections.Generic;
using EcommerceSample.Entities;
using EcommerceSample.Util;
using EcommerceSample.Repositories;
using Xunit;

namespace EcommerceSampleTests.Util
{
    public class HelperTest
    {
        private List<Category> _listToBeCalculated;
        private CategoryRepository _repository;
        public HelperTest()
        {
            _listToBeCalculated = new List<Category>()
            {
                new Category("1"),
                new Category("2"),
                new Category("3"),
                new Category("4"),
                new Category("5"),
                new Category("6")
            };

            _repository = new CategoryRepository();

            _listToBeCalculated.ForEach(c => _repository.Add(c));
        }

        [Fact]
        public void WhenGenerateIDByList_GetSuccessfulID()
        {
            var resultID = Helper.GenerateIDByList(_repository.GetAll());
            var expectedResult = _listToBeCalculated.Count;

            Assert.Equal(expectedResult, resultID);
        }
    }
}
