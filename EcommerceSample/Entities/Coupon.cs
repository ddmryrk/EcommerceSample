using EcommerceSample.Interfaces;

namespace EcommerceSample.Entities
{
    public class Coupon : PrivateObjectBase
    {
        public string Code { get; set; }
        public decimal MinimumCartAmount { get; set; }
        public DiscountOption DiscountOption { get; set; }

        public Coupon(string code, decimal minimumCartAmount, DiscountOption discountOption)
        {
            Code = code;
            MinimumCartAmount = minimumCartAmount;
            DiscountOption = discountOption;
        }
    }
}
