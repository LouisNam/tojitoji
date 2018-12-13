using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class WarehouseViewModel
    {
        public int ID { set; get; }

        [Required]
        public string Name { set; get; }

        public bool Status { set; get; }

        public string Note { set; get; }

        public int? ParentID { set; get; }
    }
}