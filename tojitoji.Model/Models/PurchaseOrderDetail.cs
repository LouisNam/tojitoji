using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("PurchaseOrderDetails")]
    public class PurchaseOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Index(IsUnique = true)]
        public int? ItemID { set; get; }

        public int? ProductID { set; get; }

        public int PurchaseOrderID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string Status { set; get; } // New;Submitted;Approved;Closed;Sourcing;Canceled;Failed;Inbound;Returned

        public decimal PurchasingPrice { set; get; } // Giá mua

        public decimal? DiscountPercent { set; get; } // Phần trăm giảm giá

        public decimal? DiscountAmount { set; get; } // Khoản giảm giá

        public string DiscountReason { set; get; } // Lý do giảm giá

        public decimal? ShippingFeeDistributor { set; get; } // Phí ship nhà cung cấp trả

        public decimal? ShippingFee { set; get; } // Phí ship nhà bán hàng trả (là mình á)

        public decimal? Subsidize { set; get; } // Tổng tiền đền bù

        public decimal? UnitCost { set; get; } // Tổng thực trả (giá hàng hóa)

        public bool? StatusPayment { set; get; } // 1: Done - 0: Not

        public int? DocumentNo { set; get; } // Phiếu chi thanh toán

        public bool? PaymentMethod { set; get; } // Loại thanh toán: 1: Tiền mặt - 0: Chuyển khoản

        public DateTime? CreatedDate { set; get; } // Ngày tạo

        public DateTime? UpdatedDate { set; get; } // Ngày cập nhật

        public DateTime? ShippingTime { set; get; } // Thời gian giao cho đơn vị vận chuyển

        public DateTime? CanceledTime { set; get; } // Thời gian hủy đơn

        public DateTime? DeliveriedETA { set; get; } // Thời gian vận chuyển dự kiến

        public DateTime? DeliveriedTime { set; get; } // Thời gian vận chuyển

        public DateTime? FailedTime { set; get; } // Thời gian rớt đơn

        public DateTime? PaidTime { set; get; } // Thời gian thanh toán

        public string ShippingParcel { set; get; } // Mã kiện hàng giao đi

        public string TKN { set; get; } // Default: 156

        public string TKC { set; get; } // Default: 331

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        [ForeignKey("PurchaseOrderID")]
        public virtual PurchaseOrder PurchaseOrder { set; get; }
    }
}