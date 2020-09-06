using EcommerceSample.Interfaces.Repositories;

namespace EcommerceSample.Util
{
    public class CouponManager
    {
        private IShoppingCartRepository _shoppingCartRepository;
        public CouponManager(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public decimal GetCouponDiscountAmountForCart()
        {
            var cart = _shoppingCartRepository.GetCart();

            if (cart.Coupon == null)
                return 0;

            var totalAmountAfterCampaignDiscount = cart.GetDiscountedAmount();

            if (cart.Coupon.MinimumCartAmount > totalAmountAfterCampaignDiscount)
                return 0;

            var discount = cart.Coupon.DiscountOption;

            return discount?.CalculateDiscountedValue(totalAmountAfterCampaignDiscount) ?? 0;
        }
    }
}
