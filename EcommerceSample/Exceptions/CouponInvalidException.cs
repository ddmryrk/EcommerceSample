using System;
namespace EcommerceSample.Exceptions
{
    public class CouponInvalidException : Exception
    {
        public CouponInvalidException(string message) : base(message)
        {
        }
    }
}
