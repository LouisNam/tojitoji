using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("SalesOrders")]
    public class SalesOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public DateTime CreatedDate { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string DocumentID { set; get; } // Mã chứng từ

        public string Description { set; get; }

        public int CustomerID { set; get; }

        public int? StaffID { set; get; }

        [MaxLength(256)]
        public string SalesPlace { set; get; } // Nơi bán

        public int WarehouseID { set; get; }

        public string PartnerOrderID { set; get; }

        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Human Customer { set; get; }

        [ForeignKey("StaffID")]
        public virtual Human Staff { set; get; }

        [ForeignKey("WarehouseID")]
        public virtual Warehouse Warehouse { set; get; }
    }
}