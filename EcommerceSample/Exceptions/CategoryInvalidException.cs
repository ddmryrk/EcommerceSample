using System;
namespace EcommerceSample.Exceptions
{
    public class CategoryInvalidException : Exception
    {
        public CategoryInvalidException(string message) : base(message) 
        {
        }
    }
}
