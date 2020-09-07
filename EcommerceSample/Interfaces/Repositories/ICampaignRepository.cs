using System.Collections.Generic;
using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Repositories
{
    public interface ICampaignRepository
    {
        Result Add(Campaign campaign);
        List<Campaign> GetAll();
        Campaign GetByID(int id);
        Campaign GetByCategoryID(int id);        
    }
}
