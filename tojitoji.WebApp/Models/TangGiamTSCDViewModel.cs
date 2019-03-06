using System;
using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class TangGiamTSCDViewModel
    {
        public int ID { set; get; }

        [MaxLength(20)]
        [Required]
        public string MaTSCD { set; get; }

        [MaxLength(20)]
        [Required]
        public string Type { set; get; }

        [MaxLength(256)]
        [Required]
        public string Name { set; get; }

        public DateTime NgaySuDung { set; get; }

        public int SoLuong { set; get; }

        public decimal GiaTriBanDau { set; get; }

        public decimal? GiaTriConLai { set; get; }

        public int ThoiGianSuDung { set; get; }

        public decimal GiaTriPhanBoTrongKy { set; get; }

        [MaxLength(10)]
        [Required]
        public string BoPhan { set; get; }

        [MaxLength(10)]
        [Required]
        public string PhanBo { set; get; }

        public int ThangSuDung { set; get; }

        public int NamSuDung { set; get; }

        [MaxLength(20)]
        [Required]
        public string Note { set; get; }
    }
}