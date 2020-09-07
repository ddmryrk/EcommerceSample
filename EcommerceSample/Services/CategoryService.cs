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
    public class CategoryService : ICategoryService
    {
        private ILogger _logger = LoggerFactory.CreateLogger();

        ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Result Add(Category category)
        {
            IsCategoryValid(category);

            var result = new Result();

            try
            {
                result = _categoryRepository.Add(category);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

            return result;
        }

        public Category GetByID(int id) => _categoryRepository.GetByID(id);

        private void IsCategoryValid(Category category)
        {
            if (category == null)
                throw new CategoryInvalidException("Category cannot be null");

            if (string.IsNullOrEmpty(category.Title))
                throw new CategoryInvalidException("Title is not valid");
        }

    }
}
