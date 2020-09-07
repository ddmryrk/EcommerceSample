using System.Linq;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Services;

namespace EcommerceSample.Util
{
    public class CampaignManager
    {
        private ICampaignRepository _campaignRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        public CampaignManager(ICampaignRepository campaignRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _campaignRepository = campaignRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void ApplyCampaignToCart()
        {
            var campaignService = new CampaignService(_campaignRepository);
            var campaigns = campaignService.GetAll();

            if (campaigns == null || !campaigns.Any())
                return;

            var cart = _shoppingCartRepository.GetCart();

            foreach (var cartItem in cart.Items)
            {
                decimal discountAmountByCampaign = 0;

                var categoryIDTreeOfProduct = cartItem.Product.GetCategoryIDTree();

                foreach (var categoryID in categoryIDTreeOfProduct)
                {
                    var campaignForCurrentCategory = campaignService.GetByCategoryID(categoryID);

                    if (campaignForCurrentCategory == null)
                        continue;

                    if (IsQuantityOfCartNotEnuoghForCampaign(cart, campaignForCurrentCategory, categoryID))
                        continue;

                    var discount = campaignForCurrentCategory.DiscountOption;
                    discountAmountByCampaign = discount?.CalculateDiscountedValue(cartItem.TotalAmount) ?? 0;

                    break;
                }

                cartItem.SetDiscountAmount(discountAmountByCampaign);
            }

            SetCartDiscountAmount(cart);
        }

        private bool IsQuantityOfCartNotEnuoghForCampaign(ShoppingCart cart, Campaign campaign, int categoryID)
        {
            var totalQuantityByCategoryID = cart.GetTotalQuantityByCategoryID(categoryID);
            return totalQuantityByCategoryID < campaign.MinimumCartItem;
        }

        private void SetCartDiscountAmount(ShoppingCart cart)
        {
            var totalDiscountAmount = cart.Items?.Sum(i => i.DiscountAmount) ?? 0;
            _shoppingCartRepository.SetDiscountAmount(totalDiscountAmount);
        }
        
    }
}
