using System.Linq;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;

namespace EcommerceSample.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private ShoppingCart _shoppingCart;

        public ShoppingCartRepository()
        {
            _shoppingCart = new ShoppingCart();
        }

        public ShoppingCart GetCart()
        {
            return _shoppingCart;
        }

        public void AddItem(CartItem cartItem)
        {
            _shoppingCart.Items.Add(cartItem);
            _shoppingCart.TotalAmount += cartItem.Product.Price * cartItem.Quantity;
        }

        public void IncreaseItemQuantityByProductID(int productID, int quantity)
        {
            var existingCartItem =_shoppingCart.Items.FirstOrDefault(c => c.Product.ID == productID);
            existingCartItem.Quantity += quantity;

            _shoppingCart.TotalAmount += existingCartItem.Product.Price * quantity;
        }

        public bool HasItemByProductID(int productID)
        {
            return _shoppingCart.Items.Any(c => c.Product.ID == productID);
        }

        public CartItem GetItemByProductID(int productID)
        {
            return _shoppingCart.Items.FirstOrDefault(c => c.Product.ID == productID);
        }

        public void SetCoupon(Coupon coupon)
        {
            _shoppingCart.Coupon = coupon;
        }

        public void SetDiscountAmount(decimal amount)
        {
            _shoppingCart.DiscountAmount = amount;
        }

        public void SetDiscountAmountByCoupon(decimal amount)
        {
            _shoppingCart.DiscountAmountByCoupon = amount;
        }

        public void SetDeliveryCost(decimal amount)
        {
            _shoppingCart.DeliveryCost = amount;
        }
    }
}
