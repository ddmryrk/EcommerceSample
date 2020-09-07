using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Exceptions;
using Xunit;

namespace EcommerceSampleTests.Entities
{
    public class DiscountOptionTest
    {
        public DiscountOptionTest()
        {
        }

        [Fact]
        public void WhenDefineDiscountOption_Successfully()
        {
            var discountOption = new DiscountOption(DiscountType.Amount, 50);

            Assert.NotNull(discountOption);
        }

        [Fact]
        public void WhenDefineDiscountOption_TypeIsNull_ExpectException()
        {
            var ex = Assert.Throws<DiscountInvalidException>(() => new DiscountOption((DiscountType)3, 50));
            Assert.Equal("Discount type is not valid", ex.Message);
        }

        [Fact]
        public void WhenDefineDiscountOption_RatioIsNotValid_ExpectException()
        {
            var ex = Assert.Throws<DiscountInvalidException>(() => new DiscountOption(DiscountType.Ratio, 150));
            Assert.Equal("Discount value must be between 0 and 100", ex.Message);
        }

        [Fact]
        public void WhenDefineDiscountOption_AmountIsNotValid_ExpectException()
        {
            var ex = Assert.Throws<DiscountInvalidException>(() => new DiscountOption(DiscountType.Amount, -50.0M));
            Assert.Equal("Discount value cannot be less than 0", ex.Message);
        }
    }
}
