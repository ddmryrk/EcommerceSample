using EcommerceSample.Interfaces;

namespace EcommerceSample.Entities
{
    public class CartItem : PrivateObjectBase
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            TotalAmount = product.Price * quantity;
        }

        public void SetDiscountAmount(decimal amount)
        {
            DiscountAmount = amount;
        }
    }
}
