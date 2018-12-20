using System;
using System.Data.SqlClient;
using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IPurchaseOrderDetailRepository : IRepository<PurchaseOrderDetail>
    {
        void CreatePurchaseOrderDetail(int productID, int purchaseOrderID, decimal price, int quantity, string Status, decimal? DiscountPercent, decimal? DiscountAmount, string DiscountReason, decimal? ShippingFeeDistributor, decimal? ShippingFee, decimal? Subsidize, decimal? UnitCost, bool StatusPayment, int? DocumentNo, bool? PaymentMethod, DateTime CreatedDate, DateTime? UpdatedDate, DateTime? ShippingTime, DateTime? CanceledTime, DateTime? DeliveriedETA, DateTime? DeliveriedTime, DateTime? FailedTime, DateTime? PaidTime, string ShippingParcel, string TKN, string TKC);
    }

    public class PurchaseOrderDetailRepository : RepositoryBase<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void CreatePurchaseOrderDetail(int productID, int purchaseOrderID, decimal price, int quantity, string Status, decimal? DiscountPercent, decimal? DiscountAmount, string DiscountReason, decimal? ShippingFeeDistributor, decimal? ShippingFee, decimal? Subsidize, decimal? UnitCost, bool StatusPayment, int? DocumentNo, bool? PaymentMethod, DateTime CreatedDate, DateTime? UpdatedDate, DateTime? ShippingTime, DateTime? CanceledTime, DateTime? DeliveriedETA, DateTime? DeliveriedTime, DateTime? FailedTime, DateTime? PaidTime, string ShippingParcel, string TKN, string TKC)
        {
            var cmdText = @"[CreatePurchaseOrderDetail] @ProductID = @ProductID, 
                                                        @PurchaseOrderID = @PurchaseOrderID,
                                                        @PurchasingPrice = @PurchasingPrice,
                                                        @Quantity = @Quantity,
                                                        @Status = @Status,
                                                        @DiscountPercent = @DiscountPercent,
                                                        @DiscountAmount = @DiscountAmount,
                                                        @DiscountReason = @DiscountReason,
                                                        @ShippingFeeDistributor = @ShippingFeeDistributor,
                                                        @ShippingFee = @ShippingFee,
                                                        @Subsidize = @Subsidize,
                                                        @UnitCost = @UnitCost,
                                                        @StatusPayment = @StatusPayment,
                                                        @DocumentNo = @DocumentNo,
                                                        @PaymentMethod = @PaymentMethod,
                                                        @CreatedDate = @CreatedDate,
                                                        @UpdatedDate = @UpdatedDate,
                                                        @ShippingTime = @ShippingTime,
                                                        @CanceledTime = @CanceledTime,
                                                        @DeliveriedETA = @DeliveriedETA,
                                                        @DeliveriedTime = @DeliveriedTime,
                                                        @FailedTime = @FailedTime,
                                                        @PaidTime = @PaidTime,
                                                        @ShippingParcel = @ShippingParcel,
                                                        @TKN = @TKN,
                                                        @TKC = @TKC";
            var @params = new[]{
               new SqlParameter("ProductID", productID),
               new SqlParameter("PurchaseOrderID", purchaseOrderID),
               new SqlParameter("PurchasingPrice", price),
               new SqlParameter("Quantity", quantity),
               new SqlParameter("Status", Status),
               new SqlParameter("DiscountPercent", DiscountPercent),
               new SqlParameter("DiscountAmount", DiscountAmount),
               new SqlParameter("DiscountReason", DiscountReason),
               new SqlParameter("ShippingFeeDistributor", ShippingFeeDistributor),
               new SqlParameter("ShippingFee", ShippingFee),
               new SqlParameter("Subsidize", Subsidize),
               new SqlParameter("UnitCost", UnitCost),
               new SqlParameter("StatusPayment", StatusPayment),
               new SqlParameter("DocumentNo", DocumentNo),
               new SqlParameter("PaymentMethod", PaymentMethod),
               new SqlParameter("CreatedDate", CreatedDate),
               new SqlParameter("UpdatedDate", CheckForDbNull(UpdatedDate)),
               new SqlParameter("ShippingTime", CheckForDbNull(ShippingTime)),
               new SqlParameter("CanceledTime", CheckForDbNull(CanceledTime)),
               new SqlParameter("DeliveriedETA", CheckForDbNull(DeliveriedETA)),
               new SqlParameter("DeliveriedTime", CheckForDbNull(DeliveriedTime)),
               new SqlParameter("FailedTime", CheckForDbNull(FailedTime)),
               new SqlParameter("PaidTime", CheckForDbNull(PaidTime)),
               new SqlParameter("ShippingParcel", ShippingParcel),
               new SqlParameter("TKN", TKN),
               new SqlParameter("TKC", TKC),
            };
            DbContext.Database.ExecuteSqlCommand(cmdText, @params);
        }

        public static object CheckForDbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
    }
}