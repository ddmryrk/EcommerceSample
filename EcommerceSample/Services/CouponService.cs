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
    public class CouponService : ICouponService
    {
        private ILogger _logger = LoggerFactory.CreateLogger();

        ICouponRepository _couponRepository;
        public CouponService( ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public Result Add(Coupon coupon)
        {
            var result = new Result();

            IsCouponValid(coupon);

            try
            {
                result = _couponRepository.Add(coupon);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

            return result;
        }

        public Coupon GetByID(int id)
        {
            return _couponRepository.GetByID(id);
        }

        private void IsCouponValid(Coupon coupon)
        {
            if (coupon == null)
                throw new CouponInvalidException("Coupon cannot be null");

            if (coupon.DiscountOption == null)
                throw new CouponInvalidException("Coupon discountOption cannot be null");

            if (coupon.MinimumCartAmount < 0)
                throw new CouponInvalidException("MinimumCartAmount is not valid");

            if (string.IsNullOrEmpty(coupon.Code))
                throw new CouponInvalidException("Coupon code cannot be empty");
        }
    }
}
