using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Services
{
    public interface ICartService
    {
        Result AddItem(Product product, int quantity);
        Result ApplyCoupon(Coupon coupon);
        ShoppingCart GetCart();
    }
}
