using System;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Interfaces.Services;
using EcommerceSample.Util;

namespace EcommerceSample.Services
{
    public class ShoppingCartService : ICartService
    {
        private IShoppingCartRepository _shoppingCartRepository;
        private ICampaignRepository _campaignRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ICampaignRepository campaignRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _campaignRepository = campaignRepository;
        }
                
        public Result AddItem(Product product, int quantity)
        {
            var result = new Result();

            try
            {
                if (_shoppingCartRepository.HasItemByProductID(product.ID))
                    _shoppingCartRepository.IncreaseItemQuantityByProductID(product.ID, quantity);
                else
                    _shoppingCartRepository.AddItem(new CartItem(product, quantity));

                ApplyCampaignToCart();
                ApplyCouponToCart();
                ApplyDeliveryCost();

                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                //todo log ekle
                result.IsSucceed = false;
            }

            return result;
        }

        public Result ApplyCoupon(Coupon coupon)
        {
            var result = new Result();
            try
            {
                _shoppingCartRepository.SetCoupon(coupon);
                ApplyCouponToCart();

                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                //todo log ekle
                result.Message = "Error";
            }

            return result;
        }

        private void ApplyCampaignToCart()
        {
            var campaignmanager = new CampaignManager(_campaignRepository, _shoppingCartRepository);
            campaignmanager.ApplyCampaignToCart();
        }

        private void ApplyCouponToCart()
        {
            var couponManager = new CouponManager(_shoppingCartRepository);
            var discountAmount = couponManager.GetCouponDiscountAmountForCart();
            _shoppingCartRepository.SetDiscountAmountByCoupon(discountAmount);
        }

        private void ApplyDeliveryCost()
        {
            var deliveryManager = new DeliveryManager(_shoppingCartRepository);
            var deliveryCost = deliveryManager.GetDeliveryCost();
            _shoppingCartRepository.SetDeliveryCost(deliveryCost);
        }
    }
}
