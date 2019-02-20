using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class TaiSanViewModel
    {
        public int ID { set; get; }

        [MaxLength(256)]
        public string Name { set; get; }

        public int LoaiTaiSanID { set; get; }

        public virtual LoaiTaiSanViewModel LoaiTaiSan { set; get; }
    }
}