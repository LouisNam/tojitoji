using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class LoaiTaiSanViewModel
    {
        public int ID { set; get; }

        [MaxLength(256)]
        public string Name { set; get; }
    }
}