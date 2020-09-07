using System;
using System.Collections.Generic;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Exceptions;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Interfaces.Services;
using EcommerceSample.Interfaces.Util;
using EcommerceSample.Util;

namespace EcommerceSample.Services
{
    public class CampaignService : ICampaignService
    {
        private ILogger _logger = LoggerFactory.CreateLogger();

        private ICampaignRepository _campaignRepository;
        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public Result Add(Campaign campaign)
        {
            IsCampaignValid(campaign);

            var result = new Result();

            try
            {
                result = _campaignRepository.Add(campaign);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

            return result;
        }

        public List<Campaign> GetAll()
        {
            return _campaignRepository.GetAll();
        }

        public Campaign GetByCategoryID(int id)
        {
            return _campaignRepository.GetByCategoryID(id);
        }

        private void IsCampaignValid(Campaign campaign)
        {
            if (campaign == null)
                throw new CampaignInvalidException("Campaign cannot be null");

            if (campaign.Category == null)
                throw new CampaignInvalidException("Campaign category cannot be null");

            if (campaign.MinimumCartItem < 0)
                throw new CampaignInvalidException("MinimumCartItem is not valid");

            if (_campaignRepository.GetByCategoryID(campaign.Category.ID) != null)
                throw new CampaignInvalidException("There is a campaign for this category");
        }
    }
}
