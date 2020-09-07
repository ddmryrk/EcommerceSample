using System;
using System.Collections.Generic;
using EcommerceSample.Constants;
using EcommerceSample.Entities;

namespace EcommerceSample.Interfaces.Services
{
    public interface ICampaignService
    {
        Result Add(Campaign campaign);
        Campaign GetByCategoryID(int id);
        List<Campaign> GetAll();
    }
}
