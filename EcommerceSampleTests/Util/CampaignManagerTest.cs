using Xunit;
using Moq;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Entities;
using EcommerceSample.Services;
using EcommerceSample.Constants;
using System.Collections.Generic;
using EcommerceSample.Util;
using EcommerceSample.Repositories;

namespace EcommerceSampleTests.Util
{
    public class CampaignManagerTest
    {
        CategoryRepository _categoryRepository = new CategoryRepository();
        ProductRepository _productRepository = new ProductRepository();
        CampaignRepository _campaignRepository = new CampaignRepository();

        CategoryService _categoryService;
        ProductService _productService;
        CampaignService _campaignService;

        ICouponRepository _couponRepository;
        
        public CampaignManagerTest()
        {
            _categoryService = new CategoryService(_categoryRepository);
            _categoryService.Add(new Category("Phone"));
            var category = _categoryService.GetByID(0);

            _productService = new ProductService(_productRepository);
            _productService.Add(new Product("IPhone 5S", 500, category));
            _productService.Add(new Product("IPhone 8", 2000, category));
            _productService.Add(new Product("IPhone 11", 5000, category));

            _campaignService = new CampaignService(_campaignRepository);
            _campaignService.Add(new Campaign("Phone %20", category, 2,
                                    new DiscountOption(DiscountType.Ratio, 20)));

            _couponRepository = new Mock<ICouponRepository>().Object;
        }

        [Fact]
        public void WhenApplyCampaignToCart_NoCampaign()
        {
            var cartRepository = new Mock<IShoppingCartRepository>();
            var campaignRepository = new Mock<ICampaignRepository>();
            campaignRepository.Setup(c => c.GetAll())
                .Returns(new List<Campaign>());

            var manager = new CampaignManager(campaignRepository.Object, cartRepository.Object);
            manager.ApplyCampaignToCart();

            cartRepository.Verify(c => c.GetCart(), Times.Never);
        }

        [Fact]
        public void WhenApplyCampaignToCart_SingleCategoryItemForCampaign_ExpectSuccess()
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository, _campaignRepository, _couponRepository);
            shoppingCartService.AddItem(_productService.GetByID(0), 2);

            var campaignManager = new CampaignManager(_campaignRepository, shoppingCartRepository);
            campaignManager.ApplyCampaignToCart();

            var expectedResult = 1000 * 20 / 100; 

            var finalCart = shoppingCartService.GetCart();

            Assert.Equal(expectedResult, finalCart.DiscountAmount);
            Assert.Equal(expectedResult, finalCart.Items[0].DiscountAmount);
        }

        [Fact]
        public void WhenApplyCampaignToCart_MultipleCategoryItemForCampaign_ExpectSuccess()
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository, _campaignRepository, _couponRepository);
            shoppingCartService.AddItem(_productService.GetByID(0), 1);
            shoppingCartService.AddItem(_productService.GetByID(1), 1);

            var campaignManager = new CampaignManager(_campaignRepository, shoppingCartRepository);
            campaignManager.ApplyCampaignToCart();

            var expectedResult = 2500 * 20 / 100;

            var finalCart = shoppingCartService.GetCart();

            Assert.Equal(expectedResult, finalCart.DiscountAmount);
        }

        [Fact]
        public void WhenApplyCampaignToCart_NotEnoughItemCount_ExpectNoDiscount()
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository, _campaignRepository, _couponRepository);
            shoppingCartService.AddItem(_productService.GetByID(0), 1);

            var campaignManager = new CampaignManager(_campaignRepository, shoppingCartRepository);
            campaignManager.ApplyCampaignToCart();

            var finalCart = shoppingCartService.GetCart();

            Assert.Equal(0, finalCart.DiscountAmount);
            Assert.Equal(0, finalCart.Items[0].DiscountAmount);
        }
    }
}
