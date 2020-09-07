using System.Collections.Generic;
using System.Linq;

namespace EcommerceSample.Entities
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; }
        public Coupon Coupon { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountAmountByCoupon { get; set; }
        public decimal DeliveryCost { get; set; }

        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }

        public decimal GetDiscountedAmount() => TotalAmount - DiscountAmount;

        public int GetTotalQuantityByCategoryID(int categoryID)
        {
            return Items.Where(i => i.Product.GetCategoryIDTree().Contains(categoryID))?.Sum(q => q.Quantity) ?? 0;
        }
    }
}
