using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("InventoryTransactions")]
    public class InventoryTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public DateTime ModifiedDate { set; get; } // Thời gian cập nhật giao dịch

        public DateTime CreatedDate { set; get; } // Thời gian tạo giao dịch

        public bool Type { set; get; } // Loại giao dịch (1: Nhập, 0: xuất)

        public int? ItemID { set; get; } // Mã hàng

        public int? ParcelID { set; get; } // Mã gói hàng

        public int? WarehouseID { set; get; } // Mã kho

        public bool Status { set; get; }

        public string Note { set; get; }

        [ForeignKey("WarehouseID")]
        public virtual Warehouse Warehouse { set; get; }
    }
}