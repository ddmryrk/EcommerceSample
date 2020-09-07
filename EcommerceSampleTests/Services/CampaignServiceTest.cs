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
        Campaign defaultCampaign;
        public CampaignServiceTest()
        {
            var discount = new DiscountOption(DiscountType.Ratio, 25);
            var category = new Category("Food");
            defaultCampaign = new Campaign("Campaign 1", category, 1, discount);
        }

        [Fact]
        public void WhenAddCampaign_AddedSuccesfully()
        {
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            campaignRepositoryMock
                .Setup(p => p.Add(It.IsAny<Campaign>()))
                .Returns(new Result(true));            

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var result = campaignService.Add(defaultCampaign);

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

            var campaign = defaultCampaign;
            campaign.Category = null;

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(campaign));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddCampaignMinimumCartItemIsNegative_ExpectException()
        {
            var expectedErrorMessage = "MinimumCartItem is not valid";
            var campaignRepositoryMock = new Mock<ICampaignRepository>();

            var campaign = defaultCampaign;
            campaign.MinimumCartItem = -1;

            var campaignService = new CampaignService(campaignRepositoryMock.Object);
            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(campaign));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }

        [Fact]
        public void WhenAddCampaignExistAnotherCampaignForCategory_ExpectException()
        {
            var expectedErrorMessage = "There is a campaign for this category";
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            campaignRepositoryMock
                .Setup(c => c.GetByCategoryID(It.IsAny<int>()))
                .Returns(defaultCampaign);            

            var campaignService = new CampaignService(campaignRepositoryMock.Object);

            var ex = Assert.Throws<CampaignInvalidException>(() => campaignService.Add(defaultCampaign));
            Assert.Equal(expectedErrorMessage, ex.Message);
        }
    }
}
