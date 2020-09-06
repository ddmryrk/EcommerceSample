using System.Linq;
using EcommerceSample.Constants;
using EcommerceSample.Interfaces.Repositories;

namespace EcommerceSample.Util
{
    public class DeliveryManager
    {
        private IShoppingCartRepository _shoppingCartRepository;
        public DeliveryManager(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }        

        public decimal GetDeliveryCost()
        {
            return DeliveryConstants.DELIVERY_COST +
                (DeliveryConstants.DELIVERY_COST_FOR_DELIVERY_COUNT * GetDeliveryCount()) +
                (DeliveryConstants.DELIVERY_COST_FOR_PRODUCT_COUNT * GetProductCount());
        }

        private int GetDeliveryCount()
        {
            var cart = _shoppingCartRepository.GetCart();
            return cart?.Items?.Select(i => i.Product.CategoryID)?.Distinct()?.Count() ?? 0;
        }

        private int GetProductCount()
        {
            var cart = _shoppingCartRepository.GetCart();
            return cart?.Items?.Sum(q => q.Quantity) ?? 0;
        }
    }
}
