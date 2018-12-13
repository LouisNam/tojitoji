using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("CampaignSKUs")]
    public class CampaignSKU
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int SKUID { set; get; }

        public decimal Price { set; get; } // Giá campaign

        [ForeignKey("SKUID")]
        public virtual SKU SKU { set; get; }
    }
}