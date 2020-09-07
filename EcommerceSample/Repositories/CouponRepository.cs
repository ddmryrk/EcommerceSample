using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Interfaces.Util;
using EcommerceSample.Util;

namespace EcommerceSample.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private ILogger _logger = LoggerFactory.CreateLogger();

        private List<Coupon> _coupons;
        public CouponRepository()
        {
            _coupons = new List<Coupon>();
        }

        public Result Add(Coupon coupon)
        {
            var result = new Result();

            try
            {
                coupon.ID = Helper.GenerateIDByList(_coupons);
                _coupons.Add(coupon);

                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                result.IsSucceed = false;
            }

            return result;
        }

        public Coupon GetByID(int id)
        {
            return _coupons.FirstOrDefault(c => c.ID == id);
        }
    }
}
