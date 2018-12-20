using System;

namespace tojitoji.WebApp.Models
{
    public class PurchaseOrderDetailViewModel
    {
        public int ID { set; get; }

        public int? ItemID { set; get; }

        public int ProductID { set; get; }

        public int PurchaseOrderID { set; get; }

        public string Status { set; get; }

        public decimal PurchasingPrice { set; get; }

        public decimal? DiscountPercent { set; get; }

        public decimal? DiscountAmount { set; get; }

        public string DiscountReason { set; get; }

        public decimal? ShippingFeeDistributor { set; get; }

        public decimal? ShippingFee { set; get; }

        public decimal? Subsidize { set; get; }

        public decimal? UnitCost { set; get; }

        public bool StatusPayment { set; get; }

        public int? DocumentNo { set; get; }

        public bool? PaymentMethod { set; get; }

        public DateTime CreatedDate { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public DateTime? ShippingTime { set; get; }

        public DateTime? CanceledTime { set; get; }

        public DateTime? DeliveriedETA { set; get; }

        public DateTime? DeliveriedTime { set; get; }

        public DateTime? FailedTime { set; get; }

        public DateTime? PaidTime { set; get; }

        public string ShippingParcel { set; get; }

        public string TKN { set; get; }

        public string TKC { set; get; }

        public virtual ProductViewModel Product { set; get; }

        public virtual PurchaseOrderViewModel PurchaseOrder { set; get; }

        public int Quantity { set; get; }
    }
}