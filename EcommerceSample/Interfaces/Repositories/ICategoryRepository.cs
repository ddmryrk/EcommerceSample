using EcommerceSample.Constants;
using EcommerceSample.Entities;
using System.Collections.Generic;

namespace EcommerceSample.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Result Add(Category category);
        Category GetByID(int id);
        List<Category> GetAll();
    }
}
