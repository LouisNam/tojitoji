namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPurchaseOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentTypes",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Name = c.String(maxLength: 30),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseOrderDetailReturns",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ReturnedTime = c.DateTime(),
                        ReturnedParcel = c.String(),
                        ReturnedAmount = c.Decimal(precision: 18, scale: 2),
                        ReturnedAmountTime = c.DateTime(),
                        PaymentMethod = c.Boolean(nullable: false),
                        TKN = c.String(),
                        TKC = c.String(),
                        StatusPayment = c.Boolean(nullable: false),
                        DocumentNo = c.Int(),
                        PurchaseOrderReturnID = c.Int(),
                        CreatedDate = c.DateTime(),
                        DocumentTypeID = c.String(maxLength: 2, unicode: false),
                        MaChungTu = c.Int(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeID)
                .ForeignKey("dbo.PurchaseOrderDetails", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DocumentTypeID);
            
            CreateTable(
                "dbo.PurchaseOrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(),
                        ProductID = c.Int(),
                        PurchaseOrderID = c.Int(nullable: false),
                        Status = c.String(maxLength: 20, unicode: false),
                        PurchasingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(precision: 18, scale: 2),
                        DiscountReason = c.String(),
                        ShippingFeeDistributor = c.Decimal(precision: 18, scale: 2),
                        ShippingFee = c.Decimal(precision: 18, scale: 2),
                        Subsidize = c.Decimal(precision: 18, scale: 2),
                        UnitCost = c.Decimal(precision: 18, scale: 2),
                        StatusPayment = c.Boolean(),
                        DocumentNo = c.Int(),
                        PaymentMethod = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        ShippingTime = c.DateTime(),
                        CanceledTime = c.DateTime(),
                        DeliveriedETA = c.DateTime(),
                        DeliveriedTime = c.DateTime(),
                        FailedTime = c.DateTime(),
                        PaidTime = c.DateTime(),
                        ShippingParcel = c.String(),
                        TKN = c.String(),
                        TKC = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.PurchaseOrderID);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        DocumentTypeID = c.String(maxLength: 2, unicode: false),
                        DocumentID = c.String(),
                        Description = c.String(),
                        SupplierID = c.Int(),
                        SubmittedByID = c.Int(),
                        SubmittedDate = c.DateTime(),
                        ApprovedByID = c.Int(),
                        ApprovedDate = c.DateTime(),
                        Note = c.String(),
                        PurchasePlace = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Humans", t => t.ApprovedByID)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeID)
                .ForeignKey("dbo.Humans", t => t.SubmittedByID)
                .ForeignKey("dbo.Humans", t => t.SupplierID)
                .Index(t => t.DocumentTypeID)
                .Index(t => t.SupplierID)
                .Index(t => t.SubmittedByID)
                .Index(t => t.ApprovedByID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderDetailReturns", "ID", "dbo.PurchaseOrderDetails");
            DropForeignKey("dbo.PurchaseOrderDetails", "PurchaseOrderID", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "SupplierID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrders", "SubmittedByID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrders", "DocumentTypeID", "dbo.DocumentTypes");
            DropForeignKey("dbo.PurchaseOrders", "ApprovedByID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderDetailReturns", "DocumentTypeID", "dbo.DocumentTypes");
            DropIndex("dbo.PurchaseOrders", new[] { "ApprovedByID" });
            DropIndex("dbo.PurchaseOrders", new[] { "SubmittedByID" });
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierID" });
            DropIndex("dbo.PurchaseOrders", new[] { "DocumentTypeID" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "PurchaseOrderID" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "ProductID" });
            DropIndex("dbo.PurchaseOrderDetailReturns", new[] { "DocumentTypeID" });
            DropIndex("dbo.PurchaseOrderDetailReturns", new[] { "ID" });
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseOrderDetails");
            DropTable("dbo.PurchaseOrderDetailReturns");
            DropTable("dbo.DocumentTypes");
        }
    }
}
