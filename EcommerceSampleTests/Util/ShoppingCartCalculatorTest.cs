using System;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Util;
using Moq;
using Xunit;

namespace EcommerceSampleTests.Util
{
    public class ShoppingCartCalculatorTest
    {
        public ShoppingCartCalculatorTest()
        {
        }

        [Theory]
        [InlineData(100, 5, 10, 10, 95)]
        [InlineData(100, 5.5, 9.5, 15, 100)]
        [InlineData(100, 90, 30, 5, 0)]
        public void CanGetAmountForPaymentTheory(decimal totalAmount, decimal discountAmount, decimal discountAmountByCoupon, decimal deliveryCost, decimal expected)
        {
            var testResultCart = new ShoppingCart()
            {
                TotalAmount = totalAmount,
                DiscountAmount = discountAmount,
                DiscountAmountByCoupon = discountAmountByCoupon,
                DeliveryCost = deliveryCost
            };

            var shoppingCartRepository = new Mock<IShoppingCartRepository>();
            shoppingCartRepository
                .Setup(s => s.GetCart())
                .Returns(testResultCart);

            var calculator = new ShoppingCartCalculator(shoppingCartRepository.Object);
            var result = calculator.GetAmountForPayment();
            Assert.Equal(expected, result);
        }

    }
}
