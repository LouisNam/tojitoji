namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalesOrderTables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            CreateTable(
                "dbo.SalesOrderDetailReturns",
                c => new
                    {
                        SalesOrderDetailID = c.Int(nullable: false, identity: true),
                        ReturnedTime = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountTime = c.DateTime(),
                        Parcel = c.String(),
                        TKNGiaVon = c.String(maxLength: 10, unicode: false),
                        TKCGiaVon = c.String(maxLength: 10, unicode: false),
                        TKNGiaBan = c.String(maxLength: 10, unicode: false),
                        TKCGiaBan = c.String(maxLength: 10, unicode: false),
                        DocumentNo = c.Int(),
                        SalesOrderReturnID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        DocumentTypeID = c.String(maxLength: 2, unicode: false),
                        DocumentID = c.String(maxLength: 20, unicode: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SalesOrderDetailID);
            
            CreateTable(
                "dbo.SalesOrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SalesOrderID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        SKU = c.Int(),
                        Status = c.String(maxLength: 20, unicode: false),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(precision: 18, scale: 2),
                        DiscountReason = c.String(),
                        ShippingFee = c.Decimal(precision: 18, scale: 2),
                        ShippingFreeCustomer = c.Decimal(precision: 18, scale: 2),
                        OtherFee = c.Decimal(precision: 18, scale: 2),
                        CustomerPaid = c.Decimal(precision: 18, scale: 2),
                        Refund = c.Decimal(precision: 18, scale: 2),
                        StatusPayment = c.Boolean(nullable: false),
                        PaymentMethod = c.String(maxLength: 20, unicode: false),
                        PartnerOrderItemID = c.String(),
                        CustomerComment = c.String(),
                        CustomerRating = c.Int(),
                        Note = c.String(),
                        UpdatedTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        ShippingTime = c.DateTime(),
                        CanceledTime = c.DateTime(),
                        DeliveriedETA = c.DateTime(),
                        DeliveriedTime = c.DateTime(),
                        FailedTime = c.DateTime(),
                        PaidTime = c.DateTime(),
                        BillingName = c.String(),
                        BillingPhoneNumber1 = c.String(maxLength: 20, unicode: false),
                        BillingPhoneNumber2 = c.String(maxLength: 20, unicode: false),
                        BillingAddress = c.String(maxLength: 20),
                        BillingWard = c.String(maxLength: 20),
                        BillingDistrict = c.String(maxLength: 20),
                        BillingCity = c.String(maxLength: 20),
                        BillingState = c.String(maxLength: 20),
                        BillingCountry = c.String(maxLength: 20),
                        BillingZIP = c.String(maxLength: 20),
                        ShippingName = c.String(),
                        ShippingPhoneNumber1 = c.String(maxLength: 20, unicode: false),
                        ShippingPhoneNumber2 = c.String(maxLength: 20, unicode: false),
                        ShippingAddress = c.String(maxLength: 20),
                        ShippingWard = c.String(maxLength: 20),
                        ShippingDistrict = c.String(maxLength: 20),
                        ShippingCity = c.String(maxLength: 20),
                        ShippingState = c.String(maxLength: 20),
                        ShippingCountry = c.String(maxLength: 20),
                        ShippingZIP = c.String(maxLength: 20),
                        TrackingCode = c.String(maxLength: 20, unicode: false),
                        TrackingURL = c.String(maxLength: 256, unicode: false),
                        ShippingProvider = c.String(),
                        ShippingMethod = c.String(),
                        ShippingParcel = c.String(),
                        TKNGiaVon = c.String(maxLength: 10, unicode: false),
                        TKCGiaVon = c.String(maxLength: 10, unicode: false),
                        TKNGiaBan = c.String(maxLength: 10, unicode: false),
                        TKCGiaBan = c.String(maxLength: 10, unicode: false),
                        DocumentNo = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SalesOrders", t => t.SalesOrderID, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKU)
                .Index(t => t.SalesOrderID)
                .Index(t => t.ItemID, unique: true)
                .Index(t => t.SKU);
            
            CreateTable(
                "dbo.SalesOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        DocumentTypeID = c.String(maxLength: 2, unicode: false),
                        DocumentID = c.String(maxLength: 20, unicode: false),
                        Description = c.String(),
                        CustomerID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        SalesPlace = c.String(maxLength: 256),
                        WarehouseID = c.Int(nullable: false),
                        PartnerOrderID = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Humans", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeID)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseID, cascadeDelete: true)
                .Index(t => t.DocumentTypeID)
                .Index(t => t.CustomerID)
                .Index(t => t.WarehouseID);
            
            AlterColumn("dbo.Accounts", "ID", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddPrimaryKey("dbo.Accounts", "ID");
            CreateIndex("dbo.PurchaseOrderDetails", "ItemID", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesOrderDetails", "SKU", "dbo.SKUs");
            DropForeignKey("dbo.SalesOrderDetails", "SalesOrderID", "dbo.SalesOrders");
            DropForeignKey("dbo.SalesOrders", "WarehouseID", "dbo.Warehouses");
            DropForeignKey("dbo.SalesOrders", "DocumentTypeID", "dbo.DocumentTypes");
            DropForeignKey("dbo.SalesOrders", "CustomerID", "dbo.Humans");
            DropIndex("dbo.SalesOrders", new[] { "WarehouseID" });
            DropIndex("dbo.SalesOrders", new[] { "CustomerID" });
            DropIndex("dbo.SalesOrders", new[] { "DocumentTypeID" });
            DropIndex("dbo.SalesOrderDetails", new[] { "SKU" });
            DropIndex("dbo.SalesOrderDetails", new[] { "ItemID" });
            DropIndex("dbo.SalesOrderDetails", new[] { "SalesOrderID" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "ItemID" });
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "ID", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.SalesOrders");
            DropTable("dbo.SalesOrderDetails");
            DropTable("dbo.SalesOrderDetailReturns");
            AddPrimaryKey("dbo.Accounts", "ID");
        }
    }
}
