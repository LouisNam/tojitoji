using System;

namespace tojitoji.WebApp.Models
{
    public class CampaignViewModel
    {
        public int CampaignID { set; get; }

        public string Name { set; get; }

        public DateTime FromTime { set; get; }

        public DateTime ToTime { set; get; }

        public virtual CampaignSKUViewModel CampaignSKU { set; get; }
    }
}