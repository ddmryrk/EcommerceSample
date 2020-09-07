using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Interfaces.Util;
using EcommerceSample.Util;

namespace EcommerceSample.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private List<Category> _categories;
        public CategoryRepository()
        {
            _categories = new List<Category>();
        }

        public Result Add(Category category)
        {
            var result = new Result();

            category.ID = Helper.GenerateIDByList(_categories);
            _categories.Add(category);

            result.IsSucceed = true;           

            return result;
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category GetByID(int id)
        {
            return _categories.FirstOrDefault(c => c.ID == id);
        }
    }
}
