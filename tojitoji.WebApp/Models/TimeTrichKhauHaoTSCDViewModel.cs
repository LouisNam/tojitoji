using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class TimeTrichKhauHaoTSCDViewModel
    {
        public int ID { set; get; }

        [MaxLength(256)]
        [Required]
        public string DanhMucNhomTSCD { set; get; }

        [MaxLength(256)]
        [Required]
        public string NhomTSCD { set; get; }

        public int TimeMin { set; get; }

        public int TimeMax { set; get; }
    }
}