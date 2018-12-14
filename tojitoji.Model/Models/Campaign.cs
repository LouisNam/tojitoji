using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Campaigns")]
    public class Campaign
    {
        [Key]
        public int CampaignID { set; get; }

        public string Name { set; get; }

        public DateTime FromTime { set; get; }

        public DateTime ToTime { set; get; }

        [ForeignKey("CampaignID")]
        public virtual CampaignSKU CampaignSKU { set; get; }
    }
}