using System;
namespace EcommerceSample.Exceptions
{
    public class CampaignInvalidException : Exception
    {
        public CampaignInvalidException(string message) : base(message)
        {
        }
    }
}
