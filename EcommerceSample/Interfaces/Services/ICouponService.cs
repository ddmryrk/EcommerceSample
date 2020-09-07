using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Services
{
    public interface ICouponService
    {
        Result Add(Coupon coupon);
        Coupon GetByID(int id);
    }
}
