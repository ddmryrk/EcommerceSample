using System;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Exceptions;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Interfaces.Services;
using EcommerceSample.Interfaces.Util;
using EcommerceSample.Util;

namespace EcommerceSample.Services
{
    public class ShoppingCartService : ICartService
    {
        private ILogger _logger = LoggerFactory.CreateLogger();

        private IShoppingCartRepository _shoppingCartRepository;
        private ICampaignRepository _campaignRepository;
        private ICouponRepository _couponRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
                                    ICampaignRepository campaignRepository,
                                    ICouponRepository couponRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _campaignRepository = campaignRepository;
            _couponRepository = couponRepository;
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
                _logger.Log(ex);
            }

            return result;
        }

        public Result ApplyCoupon(Coupon coupon)
        {
            var result = new Result();
            try
            {
                IsCouponExists(coupon);

                _shoppingCartRepository.SetCoupon(coupon);
                ApplyCouponToCart();

                result.IsSucceed = true;
            }
            catch (CouponInvalidException ex)
            {
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                result.Message = "Error";
            }

            return result;
        }

        public ShoppingCart GetCart()
        {
            return _shoppingCartRepository.GetCart();
        }

        private void IsCouponExists(Coupon coupon)
        {
            var couponService = new CouponService(_couponRepository);
            var controlledCoupon = couponService.GetByID(coupon.ID);

            if (controlledCoupon == null)
                throw new CouponInvalidException("Coupon is not valid");
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
