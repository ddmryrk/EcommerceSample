using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Services
{
    public interface ICartService
    {
        public Result AddItem(Product product, int quantity);
        public Result ApplyCoupon(Coupon coupon);
        ShoppingCart GetCart();
    }
}
