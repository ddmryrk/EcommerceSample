using System;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Exceptions;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Services;
using Moq;
using Xunit;

namespace EcommerceSampleTests.Services
{
    public class CampaignServiceTest
    {
        public CampaignServiceTest()
        {
        }

        [Fact]
        public void WhenAddCampaign_AddedSuccesfully()
        {
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            campaignRepositoryMock
                .Setup(p => p.Add(It.IsAny<Campaign>()))
                .Returns(new Result(true));

            var discount = new DiscountOption(DiscountType.Ratio);
            discount.SetValue(25);
            var category = new Category("Food");
            var campaign = new Campaign("Campaign 1", category, 1, discount);

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var result = campaignService.Add(campaign);

            Assert.True(result.IsSucceed);
        }

        [Fact]
        public void WhenAddNullCampaign_ExpectException()
        {
            var expectedErrorMessage = "Campaign cannot be null";
            var campaignRepositoryMock = new Mock<ICampaignRepository>();

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(null));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddCampaignCategoryNull_ExpectException()
        {
            var expectedErrorMessage = "Campaign category cannot be null";
            var campaignRepositoryMock = new Mock<ICampaignRepository>();

            var discount = new DiscountOption(DiscountType.Ratio);
            discount.SetValue(25);
            var campaign = new Campaign("Campaign 1", null, 1, discount);

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(campaign));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddCampaignMinimumCartItemIsNegative_ExpectException()
        {
            var expectedErrorMessage = "MinimumCartItem is not valid";
            var campaignRepositoryMock = new Mock<ICampaignRepository>();

            var discount = new DiscountOption(DiscountType.Ratio);
            discount.SetValue(25);
            var category = new Category("Food");
            var campaign = new Campaign("Campaign 1", category, -1, discount);

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(campaign));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddCampaignExistAnotherCampaignForCategory_ExpectException()
        {
            var discount = new DiscountOption(DiscountType.Ratio);
            discount.SetValue(25);
            var category = new Category("Food");
            var campaign = new Campaign("Campaign 1", category, 1, discount);

            var expectedErrorMessage = "There is a campaign for this category";
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            campaignRepositoryMock
                .Setup(c => c.GetByCategoryID(It.IsAny<int>()))
                .Returns(campaign);            

            var campaignService = new CampaignService(campaignRepositoryMock.Object);

            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(campaign));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }
    }
}
