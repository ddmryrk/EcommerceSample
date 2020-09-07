using System.Collections.Generic;
using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Services
{
    public interface ICategoryService
    {
        Result Add(Category category);
        Category GetByID(int id);
    }
}
