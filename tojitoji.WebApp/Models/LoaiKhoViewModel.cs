using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class LoaiKhoViewModel
    {
        public int ID { set; get; }

        [Required]
        [MaxLength(10)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Description { set; get; }
    }
}