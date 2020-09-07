using System;
namespace EcommerceSample.Exceptions
{
    public class DiscountInvalidException : Exception
    {
        public DiscountInvalidException(string message) : base(message)
        {
        }
    }
}
