using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Documents")]
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; } // Loại chứng từ

        public DateTime Date { set; get; } // Ngày chứng từ

        public string Description { set; get; }

        public int HumanID { set; get; }

        public string Serial { set; get; }

        public string BillNo { set; get; } // Số hóa đơn

        public DateTime? BillDate { set; get; } // Ngày hóa đơn

        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { set; get; }

        [ForeignKey("HumanID")]
        public virtual Human Human { set; get; }
    }
}