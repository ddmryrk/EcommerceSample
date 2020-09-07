using System;
namespace EcommerceSample.Exceptions
{
    public class ProductInvalidException : Exception
    {
        public ProductInvalidException(string message) : base(message)
        {
        }
    }
}
