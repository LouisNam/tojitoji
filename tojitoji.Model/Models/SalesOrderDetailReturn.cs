using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("SalesOrderDetailReturns")]
    public class SalesOrderDetailReturn
    {
        [Key]
        public int ID { set; get; }

        public DateTime ReturnedTime { set; get; }

        public decimal Amount { set; get; }

        public DateTime? AmountTime { set; get; }

        public string Parcel { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string TKNGiaVon { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string TKCGiaVon { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string TKNGiaBan { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string TKCGiaBan { set; get; }

        public int? DocumentNo { set; get; }

        public int SalesOrderReturnID { set; get; }

        public DateTime CreatedDate { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string DocumentID { set; get; } // Mã chứng từ

        public string Description { set; get; }

        [ForeignKey("ID")]
        public virtual SalesOrderDetail SalesOrderDetail { set; get; }

        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { set; get; }
    }
}