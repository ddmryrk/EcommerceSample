using System.Collections.Generic;
using System.Linq;
using EcommerceSample.Constants;
using EcommerceSample.Entities;
using EcommerceSample.Interfaces.Repositories;
using EcommerceSample.Util;

namespace EcommerceSample.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private List<Campaign> _campaigns;

        public CampaignRepository()
        {
            _campaigns = new List<Campaign>();
        }

        public Result Add(Campaign campaign)
        {
            var result = new Result();

            campaign.ID = Helper.GenerateIDByList(_campaigns);
            _campaigns.Add(campaign);

            result.IsSucceed = true;

            return result;
        }

        public List<Campaign> GetAll()
        {
            return _campaigns;
        }

        public Campaign GetByCategoryID(int id)
        {
            return _campaigns.Where(c => c.Category.ID == id).FirstOrDefault();
        }

        public Campaign GetByID(int id)
        {
            return _campaigns.FirstOrDefault(c => c.ID == id);
        }
    }
}
