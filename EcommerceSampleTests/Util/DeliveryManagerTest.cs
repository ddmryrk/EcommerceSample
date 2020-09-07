using System;
using Xunit;
using Moq;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Entities;
using EcommerceSample.Services;
using EcommerceSample.Constants;
using EcommerceSample.Exceptions;
using System.Collections.Generic;
using EcommerceSample.Util;
using EcommerceSample.Repositories;
using System.Linq;
namespace EcommerceSampleTests.Util
{
    public class DeliveryManagerTest
    {
        CategoryRepository _categoryRepository = new CategoryRepository();
        ProductRepository _productRepository = new ProductRepository();
        CampaignRepository _campaignRepository = new CampaignRepository();

        CategoryService _categoryService;
        ProductService _productService;
        CampaignService _campaignService;

        public DeliveryManagerTest()
        {
            _categoryService = new CategoryService(_categoryRepository);
            _categoryService.Add(new Category("Phone"));
            _categoryService.Add(new Category("Computer"));
            var category0 = _categoryService.GetByID(0);
            var category1 = _categoryService.GetByID(1);

            _productService = new ProductService(_productRepository);
            _productService.Add(new Product("IPhone 5S", 500, category0));
            _productService.Add(new Product("IPhone 8", 2000, category0));
            _productService.Add(new Product("IPhone 11", 5000, category0));
            _productService.Add(new Product("MacBook Pro", 6000, category1));

            _campaignService = new CampaignService(_campaignRepository);
            _campaignService.Add(new Campaign("Phone %20", category0, 2,
                                    new DiscountOption(DiscountType.Ratio, 20)));
        }

        [Fact]
        public void WhenGetDeliveryCost_SingleCategorySingleItem_GetSuccessfully()
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository, _campaignRepository);
            shoppingCartService.AddItem(_productService.GetByID(0), 1);

            var deliveryManager = new DeliveryManager(shoppingCartRepository);
            var deliveryCost = deliveryManager.GetDeliveryCost();
            var expectedResult = 8.0M;

            var finalCart = shoppingCartService.GetCart();

            Assert.Equal(expectedResult, deliveryCost);
            Assert.Equal(deliveryCost, finalCart.DeliveryCost);            
        }

        [Fact]
        public void WhenGetDeliveryCost_MultipleCategoryMultipleItem_GetSuccessfully()
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var shoppingCartService = new ShoppingCartService(shoppingCartRepository, _campaignRepository);
            shoppingCartService.AddItem(_productService.GetByID(0), 1);
            shoppingCartService.AddItem(_productService.GetByID(3), 1);

            var deliveryManager = new DeliveryManager(shoppingCartRepository);
            var deliveryCost = deliveryManager.GetDeliveryCost();
            var expectedResult = 11.0M;

            var finalCart = shoppingCartService.GetCart();

            Assert.Equal(expectedResult, deliveryCost);
            Assert.Equal(deliveryCost, finalCart.DeliveryCost);
        }
    }
}
