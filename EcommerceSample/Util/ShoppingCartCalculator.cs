using System;
using EcommerceSample.Interfaces.Repositories;

namespace EcommerceSample.Util
{
    public class ShoppingCartCalculator
    {
        private IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartCalculator(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public decimal GetAmountForPayment()
        {
            var cart = _shoppingCartRepository.GetCart();
            var paymentAmount = cart.TotalAmount - cart.DiscountAmount - cart.DiscountAmountByCoupon + cart.DeliveryCost;
            return paymentAmount > 0 ? paymentAmount : 0;
        }
    }
}
