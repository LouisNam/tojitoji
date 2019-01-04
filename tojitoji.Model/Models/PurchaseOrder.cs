using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("PurchaseOrders")]
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public DateTime CreatedDate { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; } // Loại chứng từ

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string DocumentID { set; get; } // Mã chứng từ

        public string Description { set; get; } // Diễn giải chung

        public int? SupplierID { set; get; } // Nhà cung cấp

        public int? SubmittedByID { set; get; } // Nhân viên mua

        public DateTime? SubmittedDate { set; get; } // Thời gian nộp

        public int? ApprovedByID { set; get; } // Nhân viên duyệt

        public DateTime? ApprovedDate { set; get; } // Thời gian duyệt

        public string Note { set; get; } // Ghi chú

        public string PurchasePlace { set; get; } // Nơi bán

        [ForeignKey("SupplierID")]
        public virtual Human Supplier { set; get; }

        [ForeignKey("SubmittedByID")]
        public virtual Human Submit { set; get; }

        [ForeignKey("ApprovedByID")]
        public virtual Human Approve { set; get; }

        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { set; get; }
    }
}