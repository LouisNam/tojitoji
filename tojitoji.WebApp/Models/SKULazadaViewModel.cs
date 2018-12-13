using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class SKULazadaViewModel
    {
        public int ID { set; get; }

        public string Lazada_SKU { set; get; }

        [MaxLength(256)]
        public string SKUName { set; get; }

        public int? SKUID { set; get; }

        public string Link { set; get; }

        public string Status { set; get; }

        public virtual SKUViewModel SKU { set; get; }
    }
}