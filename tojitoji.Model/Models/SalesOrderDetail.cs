using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("SalesOrderDetails")]
    public class SalesOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int SalesOrderID { set; get; }

        [Index(IsUnique = true)]
        public int ItemID { set; get; }

        public int? SKU { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string Status { set; get; } // Pending;Ready to Ship;CanceledBF;Outbounding;CanceledAT;Shipping;Failed;Deliveried;Returned;Refund

        public decimal SellingPrice { set; get; }

        public decimal? DiscountPercent { set; get; }

        public decimal? DiscountAmount { set; get; }

        public string DiscountReason { set; get; }

        public decimal? ShippingFee { set; get; }

        public decimal? ShippingFreeCustomer { set; get; }

        public decimal? OtherFee { set; get; }

        public decimal? CustomerPaid { set; get; }

        public decimal? Refund { set; get; }

        public bool StatusPayment { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string PaymentMethod { set; get; }

        public string PartnerOrderItemID { set; get; }

        public string CustomerComment { set; get; }

        public int? CustomerRating { set; get; }

        public string Note { set; get; }

        public DateTime? UpdatedTime { set; get; }

        public DateTime CreatedTime { set; get; }

        public DateTime? ShippingTime { set; get; }

        public DateTime? CanceledTime { set; get; }

        public DateTime? DeliveriedETA { set; get; }

        public DateTime? DeliveriedTime { set; get; }

        public DateTime? FailedTime { set; get; }

        public DateTime? PaidTime { set; get; }

        public string BillingName { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string BillingPhoneNumber1 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string BillingPhoneNumber2 { set; get; }

        [MaxLength(20)]
        public string BillingAddress { set; get; }

        [MaxLength(20)]
        public string BillingWard { set; get; }

        [MaxLength(20)]
        public string BillingDistrict { set; get; }

        [MaxLength(20)]
        public string BillingCity { set; get; }

        [MaxLength(20)]
        public string BillingState { set; get; }

        [MaxLength(20)]
        public string BillingCountry { set; get; }

        [MaxLength(20)]
        public string BillingZIP { set; get; }

        public string ShippingName { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string ShippingPhoneNumber1 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string ShippingPhoneNumber2 { set; get; }

        [MaxLength(20)]
        public string ShippingAddress { set; get; }

        [MaxLength(20)]
        public string ShippingWard { set; get; }

        [MaxLength(20)]
        public string ShippingDistrict { set; get; }

        [MaxLength(20)]
        public string ShippingCity { set; get; }

        [MaxLength(20)]
        public string ShippingState { set; get; }

        [MaxLength(20)]
        public string ShippingCountry { set; get; }

        [MaxLength(20)]
        public string ShippingZIP { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string TrackingCode { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string TrackingURL { set; get; }

        public string ShippingProvider { set; get; }

        public string ShippingMethod { set; get; }

        public string ShippingParcel { set; get; }

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

        [ForeignKey("SalesOrderID")]
        public SalesOrder SalesOrder { set; get; }

        [ForeignKey("SKU")]
        public SKU SKUs { set; get; }
    }
}