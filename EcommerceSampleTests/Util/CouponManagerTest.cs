using System;
using Xunit;
using Moq;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Entities;
using EcommerceSample.Services;
using EcommerceSample.Constants;
using EcommerceSample.Exceptions;
using EcommerceSample.Util;
using EcommerceSample.Repositories;

namespace EcommerceSampleTests.Util
{
    public class CouponManagerTest
    {
        Coupon _coupon;
        public CouponManagerTest()
        {
            var discount = new DiscountOption(DiscountType.Ratio, 10);
            _coupon = new Coupon("X01", 500, discount);
        }

        [Fact]
        public void WhenGetCouponDiscount_IfCampaignOfCartIsNull_Expect0()
        {
            var cart = new Mock<IShoppingCartRepository>();
            cart.Setup(c => c.GetCart()).Returns(new ShoppingCart());

            var manager = new CouponManager(cart.Object);
            var result = manager.GetCouponDiscountAmountForCart();

            Assert.Equal(0, result);
        }

        [Fact]
        public void WhenGetCouponDiscount_NotEnoughCartAmount_Expect0()
        {
            var campaignRepository = new Mock<ICampaignRepository>();
            var repository = new ShoppingCartRepository();
            var service = new ShoppingCartService(repository, campaignRepository.Object);
            service.ApplyCoupon(_coupon);

            var manager = new CouponManager(repository);
            var result = manager.GetCouponDiscountAmountForCart();

            Assert.Equal(0, result);
        }

        [Fact]
        public void WhenGetCouponDiscount_GetSuccessfully()
        {
            var campaignRepository = new Mock<ICampaignRepository>();
            var product = new Product("Iphone 5S", 1000, new Category("Phone"));
            var repository = new ShoppingCartRepository();
            var service = new ShoppingCartService(repository, campaignRepository.Object);
            service.AddItem(product, 1);
            service.ApplyCoupon(_coupon);
            var expectedDiscount = 100;

            var manager = new CouponManager(repository);
            var couponDiscount = manager.GetCouponDiscountAmountForCart();

            Assert.Equal(expectedDiscount, couponDiscount);
        }
    }
}
