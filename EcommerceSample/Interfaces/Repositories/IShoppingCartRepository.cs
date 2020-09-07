using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetCart();
        void AddItem(CartItem cartItem);
        void IncreaseItemQuantityByProductID(int productID, int quantity);
        bool HasItemByProductID(int productID);
        void SetCoupon(Coupon coupon);
        void SetDiscountAmount(decimal amount);
        void SetDiscountAmountByCoupon(decimal amount);
        void SetDeliveryCost(decimal amount);
    }
}
