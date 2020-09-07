using System;
using EcommerceSample.Exceptions;

namespace EcommerceSample.Constants
{
    public class DiscountOption
    {
        public DiscountType Type { get; set; }

        public decimal Value  { get; set; }

        public DiscountOption(DiscountType type, decimal value)
        {
            Type = type;
            Value = value;

            IsDiscountValid();
        }

        private void IsDiscountValid()
        {
            switch (Type)
            {
                case DiscountType.Ratio:
                    if (Value < 0 || Value > 100)
                        throw new DiscountInvalidException("Discount value must be between 0 and 100");
                    break;
                case DiscountType.Amount:
                    if (Value < 0)
                        throw new DiscountInvalidException("Discount value cannot be less than 0");
                    break;
                default:
                    throw new DiscountInvalidException("Discount type is not valid");
            }
        }

        public decimal CalculateDiscountedValue(decimal amount)
        {
            decimal result = 0;

            if (Type == DiscountType.Amount)
                result = Value;
            else if (Type == DiscountType.Ratio)
                result = amount * Value / 100;

            return result;
        }
    }
}
