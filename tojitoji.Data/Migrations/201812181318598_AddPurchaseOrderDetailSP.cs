namespace tojitoji.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPurchaseOrderDetailSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("CreatePurchaseOrderDetail",
                p => new
                {
                    ProductID = p.Int(),
                    PurchaseOrderID = p.Int(),
                    PurchasingPrice = p.Decimal(),
                    Quantity = p.Int()
                },
                @"DECLARE @stt INT, @sum INT = 0, @ModifiedDate DATETIME, @CreatedDate DATETIME
	                SET @stt=1
	                SET @ModifiedDate = getdate()
	                SET @CreatedDate = getdate()

	                WHILE @sum < @Quantity
	                BEGIN
		                WHILE EXISTS(SELECT InventoryTransactions.ItemID
					                FROM InventoryTransactions INNER JOIN PurchaseOrderDetails ON InventoryTransactions.ProductID = PurchaseOrderDetails.ProductID AND InventoryTransactions.ItemID = PurchaseOrderDetails.ItemID
					                WHERE PurchaseOrderDetails.ProductID = @ProductID AND PurchaseOrderDetails.ItemID = @stt)
			                SET @stt = @stt+1
		                INSERT INTO PurchaseOrderDetails(ItemID, ProductID, PurchaseOrderID, PurchasingPrice) VALUES (@stt,@ProductID,@PurchaseOrderID,@PurchasingPrice)
		                INSERT INTO InventoryTransactions(ModifiedDate, CreatedDate, Type, ItemID, Status, ProductID) VALUES (@ModifiedDate, @CreatedDate, 0, @stt, 0, @ProductID)
		                SET @sum = @sum + 1
	                END"
            );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.CreatePurchaseOrderDetail");
        }
    }
}