using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("PurchaseOrderDetailReturns")]
    public class PurchaseOrderDetailReturn
    {
        [Key]
        public int ID { set; get; }

        public DateTime? ReturnedTime { set; get; } // Thời gian nhận lại hàng

        public string ReturnedParcel { set; get; } // Thùng hàng trả về

        public decimal? ReturnedAmount { set; get; } // Khoản nhận lại

        public DateTime? ReturnedAmountTime { set; get; } // Thời gian nhận tiền lại

        public bool PaymentMethod { set; get; } // Loại thanh toán: 1: Tiền mặt - 0: Chuyển khoản

        public string TKN { set; get; } // Default: 331

        public string TKC { set; get; } // Default: 156

        public bool StatusPayment { set; get; } // Tình trạng thanh toán: 1: Done - 0: Not

        public int? DocumentNo { set; get; } // Phiếu thu thanh toán

        public int? PurchaseOrderReturnID { set; get; } // Số chứng từ

        public DateTime? CreatedDate { set; get; } // Ngày chứng từ

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; } // Loại Chứng Từ

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string DocumentID { set; get; } // Mã chứng từ

        public string Description { set; get; } // Diễn giải chung

        [ForeignKey("ID")]
        public virtual PurchaseOrderDetail PurchaseOrderDetail { set; get; }

        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { set; get; }
    }
}