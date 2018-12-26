namespace tojitoji.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCreatePurchaseOrderDetailsSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("CreatePurchaseOrderDetail",
                p => new
                {
                    ProductID = p.Int(),
                    PurchaseOrderID = p.Int(),
                    PurchasingPrice = p.Decimal(18, 2),
                    Quantity = p.Int(),
                    Status = p.String(),
                    DiscountPercent = p.Decimal(18, 2),
                    DiscountAmount = p.Decimal(18, 2),
                    DiscountReason = p.String(),
                    ShippingFeeDistributor = p.Decimal(18, 2),
                    ShippingFee = p.Decimal(18, 2),
                    Subsidize = p.Decimal(18, 2),
                    UnitCost = p.Decimal(18, 2),
                    StatusPayment = p.Boolean(),
                    DocumentNo = p.Int(),
                    PaymentMethod = p.Boolean(),
                    CreatedDate = p.DateTime(),
                    UpdatedDate = p.DateTime(),
                    ShippingTime = p.DateTime(),
                    CanceledTime = p.DateTime(),
                    DeliveriedETA = p.DateTime(),
                    DeliveriedTime = p.DateTime(),
                    FailedTime = p.DateTime(),
                    PaidTime = p.DateTime(),
                    ShippingParcel = p.String(),
                    TKN = p.String(),
                    TKC = p.String()
                },
                @"  DECLARE @stt INT = 1, @sum INT = 0
	                WHILE @sum < @Quantity
	                BEGIN
		                WHILE EXISTS(SELECT * FROM PurchaseOrderDetails WHERE ItemID = @stt)
    		                SET @stt = @stt+1
    	                INSERT INTO PurchaseOrderDetails(ItemID, ProductID, PurchaseOrderID, Status, PurchasingPrice, DiscountPercent, DiscountAmount, DiscountReason, ShippingFeeDistributor, ShippingFee, Subsidize, UnitCost, StatusPayment, DocumentNo, PaymentMethod, CreatedDate, UpdatedDate, ShippingTime, CanceledTime, DeliveriedETA, DeliveriedTime, FailedTime, PaidTime, ShippingParcel, TKN, TKC)
                            VALUES (@stt,@ProductID,@PurchaseOrderID,@Status, @PurchasingPrice, @DiscountPercent, @DiscountAmount, @DiscountReason, @ShippingFeeDistributor, @ShippingFee, @Subsidize, @UnitCost, @StatusPayment, @DocumentNo, @PaymentMethod, @CreatedDate, @UpdatedDate, @ShippingTime, @CanceledTime, @DeliveriedETA, @DeliveriedTime, @FailedTime, @PaidTime, @ShippingParcel, @TKN, @TKC)
    	                SET @sum = @sum + 1
	                END
                "
            );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.CreatePurchaseOrderDetail");
        }
    }
}