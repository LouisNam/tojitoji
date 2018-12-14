using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Bundles")]
    public class Bundle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Column(TypeName = "varchar")]
        [Required]
        public string BundleType { set; get; } // Gói sản phẩm

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        [Required]
        public string SKUBundle { set; get; } // Mã gói sản phẩm

        [MaxLength(256)]
        [Required]
        public string BundleName { set; get; }

        public int? ProductID { set; get; }

        public int? ProductQuantity { set; get; }

        public int? ProductNo { set; get; } // Số thứ tự của SP trong 1 bundle

        public decimal? DiscountRate { set; get; } // Giảm giá (%)

        public DateTime? SpecialFromTime { set; get; } // Thời gian bắt đầu

        public DateTime? SpecialToTime { set; get; } // Thời gian kết thúc

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
    }
}