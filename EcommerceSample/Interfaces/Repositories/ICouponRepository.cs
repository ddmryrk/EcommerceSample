using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Repositories
{
    public interface ICouponRepository
    {
        Result Add(Coupon coupon);
        Coupon GetByID(int id);
    }
}
