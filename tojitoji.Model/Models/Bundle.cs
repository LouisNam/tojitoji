using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tojitoji.Model.Models
{
    [Table("Bundles")]
    public class Bundle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string BundleType { set; get; } // Gói sản phẩm

        public string SKUBundle { set; get; } // Mã gói sản phẩm

        public string BundleName { set; get; }

        public int? ProductID { set; get; }

        public int? ProductQuantity { set; get; }

        public int? ProductNo { set; get; } // Số thứ tự của SP trong 1 bundle

        public decimal? DiscountRate { set; get; } // Giảm giá (%)

        public DateTime? SpecialFromTime { set; get; } // Thời gian bắt đầu

        public DateTime? SpecialToTime { set; get; } // Thời gian kết thúc
    }
}
