namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 10, unicode: false),
                        AccountType = c.Boolean(),
                        Account_Name = c.String(nullable: false, maxLength: 256),
                        Account_1 = c.Int(nullable: false),
                        Account_1_Name = c.String(maxLength: 256),
                        Account_2 = c.Int(),
                        Account_2_Name = c.String(maxLength: 256),
                        Account_3 = c.Int(),
                        Account_3_Name = c.String(maxLength: 256),
                        Status = c.Boolean(),
                        TKNhanKC = c.String(),
                        MaKC = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Bundles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BundleType = c.String(nullable: false, maxLength: 8000, unicode: false),
                        SKUBundle = c.String(nullable: false, maxLength: 10, unicode: false),
                        BundleName = c.String(nullable: false, maxLength: 256),
                        ProductID = c.Int(),
                        ProductQuantity = c.Int(),
                        ProductNo = c.Int(),
                        DiscountRate = c.Decimal(precision: 18, scale: 2),
                        SpecialFromTime = c.DateTime(),
                        SpecialToTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SpecialFromTime = c.DateTime(),
                        SpecialToTime = c.DateTime(),
                        Status = c.Boolean(),
                        NameEn = c.String(),
                        CategoryID = c.Int(nullable: false),
                        Brand = c.String(),
                        Model = c.String(),
                        ProductCode = c.String(),
                        ColorFamily = c.String(),
                        Size = c.String(),
                        ProductLifeTime = c.Int(),
                        Warranty = c.Int(),
                        WarrantyType = c.String(),
                        Unit = c.String(),
                        PackageContent = c.String(),
                        PackageWeight = c.Int(),
                        PackageLength = c.Int(),
                        PackageWidth = c.Int(),
                        PackageHeight = c.Int(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Origin = c.String(),
                        Video = c.String(),
                        MainImage = c.String(),
                        MoreImage = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryType = c.Int(nullable: false),
                        MacroCategory = c.String(maxLength: 8000, unicode: false),
                        CommercialCate = c.String(),
                        Name = c.String(),
                        NameEn = c.String(maxLength: 8000, unicode: false),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignID = c.Int(nullable: false),
                        Name = c.String(),
                        FromTime = c.DateTime(nullable: false),
                        ToTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignID)
                .ForeignKey("dbo.CampaignSKUs", t => t.CampaignID)
                .Index(t => t.CampaignID);
            
            CreateTable(
                "dbo.CampaignSKUs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SKUID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SKUs", t => t.SKUID, cascadeDelete: true)
                .Index(t => t.SKUID);
            
            CreateTable(
                "dbo.SKUs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(),
                        BundleID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bundles", t => t.BundleID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.BundleID);
            
            CreateTable(
                "dbo.CompanyInformations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        ShortName = c.String(),
                        SoHuuVonType = c.String(),
                        Address = c.String(),
                        MaSoThue = c.String(),
                        MSTDate = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        BankAccount = c.String(),
                        CEO = c.String(),
                        ChiefAccountant = c.String(),
                        NguoiLapBieu = c.String(),
                        Cashier = c.String(),
                        CheDoKeToanApDung = c.String(),
                        HinhThucKeToan = c.String(),
                        PPThueGTGT = c.String(),
                        PPKhauHao = c.String(),
                        PPTinhGia = c.String(),
                        PPHachToanTonKho = c.String(),
                        PPTinhGiaTonKho = c.String(),
                        VonDieuLe = c.String(),
                        ThueSuat = c.String(),
                        FinancialYear = c.String(),
                        Website = c.String(),
                        Fanpage = c.String(),
                        Youtube = c.String(),
                        Group = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Humans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        TypeCode = c.Int(nullable: false),
                        Company = c.String(maxLength: 50),
                        Gender = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        District = c.String(maxLength: 50),
                        Ward = c.String(maxLength: 50),
                        OtherContact = c.String(maxLength: 256),
                        TaxCode = c.Int(),
                        Picture = c.String(maxLength: 256),
                        Note = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateOfEntry = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HumanTypes", t => t.TypeCode, cascadeDelete: true)
                .Index(t => t.TypeCode);
            
            CreateTable(
                "dbo.HumanTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InventoryTransactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Type = c.Boolean(nullable: false),
                        ItemID = c.Int(),
                        ProductID = c.Int(),
                        ParcelID = c.Int(),
                        WarehouseID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseID)
                .Index(t => t.ProductID)
                .Index(t => t.WarehouseID);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Note = c.String(),
                        ParentID = c.Int(),
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
                .Index(t => t.ItemID, unique: true)
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
                        StaffID = c.Int(),
                        SalesPlace = c.String(maxLength: 256),
                        WarehouseID = c.Int(nullable: false),
                        PartnerOrderID = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Humans", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeID)
                .ForeignKey("dbo.Humans", t => t.StaffID)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseID, cascadeDelete: true)
                .Index(t => t.DocumentTypeID)
                .Index(t => t.CustomerID)
                .Index(t => t.StaffID)
                .Index(t => t.WarehouseID);
            
            CreateTable(
                "dbo.SKUsLazada",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Lazada_SKU = c.String(),
                        SKUName = c.String(maxLength: 256),
                        SKUID = c.Int(),
                        Link = c.String(maxLength: 8000, unicode: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SKUs", t => t.SKUID)
                .Index(t => t.SKUID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SKUsLazada", "SKUID", "dbo.SKUs");
            DropForeignKey("dbo.SalesOrderDetails", "SKU", "dbo.SKUs");
            DropForeignKey("dbo.SalesOrderDetails", "SalesOrderID", "dbo.SalesOrders");
            DropForeignKey("dbo.SalesOrders", "WarehouseID", "dbo.Warehouses");
            DropForeignKey("dbo.SalesOrders", "StaffID", "dbo.Humans");
            DropForeignKey("dbo.SalesOrders", "DocumentTypeID", "dbo.DocumentTypes");
            DropForeignKey("dbo.SalesOrders", "CustomerID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrderDetailReturns", "ID", "dbo.PurchaseOrderDetails");
            DropForeignKey("dbo.PurchaseOrderDetails", "PurchaseOrderID", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "SupplierID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrders", "SubmittedByID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrders", "DocumentTypeID", "dbo.DocumentTypes");
            DropForeignKey("dbo.PurchaseOrders", "ApprovedByID", "dbo.Humans");
            DropForeignKey("dbo.PurchaseOrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderDetailReturns", "DocumentTypeID", "dbo.DocumentTypes");
            DropForeignKey("dbo.InventoryTransactions", "WarehouseID", "dbo.Warehouses");
            DropForeignKey("dbo.InventoryTransactions", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Humans", "TypeCode", "dbo.HumanTypes");
            DropForeignKey("dbo.Campaigns", "CampaignID", "dbo.CampaignSKUs");
            DropForeignKey("dbo.CampaignSKUs", "SKUID", "dbo.SKUs");
            DropForeignKey("dbo.SKUs", "ProductID", "dbo.Products");
            DropForeignKey("dbo.SKUs", "BundleID", "dbo.Bundles");
            DropForeignKey("dbo.Bundles", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SKUsLazada", new[] { "SKUID" });
            DropIndex("dbo.SalesOrders", new[] { "WarehouseID" });
            DropIndex("dbo.SalesOrders", new[] { "StaffID" });
            DropIndex("dbo.SalesOrders", new[] { "CustomerID" });
            DropIndex("dbo.SalesOrders", new[] { "DocumentTypeID" });
            DropIndex("dbo.SalesOrderDetails", new[] { "SKU" });
            DropIndex("dbo.SalesOrderDetails", new[] { "ItemID" });
            DropIndex("dbo.SalesOrderDetails", new[] { "SalesOrderID" });
            DropIndex("dbo.PurchaseOrders", new[] { "ApprovedByID" });
            DropIndex("dbo.PurchaseOrders", new[] { "SubmittedByID" });
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierID" });
            DropIndex("dbo.PurchaseOrders", new[] { "DocumentTypeID" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "PurchaseOrderID" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "ProductID" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "ItemID" });
            DropIndex("dbo.PurchaseOrderDetailReturns", new[] { "DocumentTypeID" });
            DropIndex("dbo.PurchaseOrderDetailReturns", new[] { "ID" });
            DropIndex("dbo.InventoryTransactions", new[] { "WarehouseID" });
            DropIndex("dbo.InventoryTransactions", new[] { "ProductID" });
            DropIndex("dbo.Humans", new[] { "TypeCode" });
            DropIndex("dbo.SKUs", new[] { "BundleID" });
            DropIndex("dbo.SKUs", new[] { "ProductID" });
            DropIndex("dbo.CampaignSKUs", new[] { "SKUID" });
            DropIndex("dbo.Campaigns", new[] { "CampaignID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Bundles", new[] { "ProductID" });
            DropTable("dbo.SKUsLazada");
            DropTable("dbo.SalesOrders");
            DropTable("dbo.SalesOrderDetails");
            DropTable("dbo.SalesOrderDetailReturns");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseOrderDetails");
            DropTable("dbo.PurchaseOrderDetailReturns");
            DropTable("dbo.Warehouses");
            DropTable("dbo.InventoryTransactions");
            DropTable("dbo.HumanTypes");
            DropTable("dbo.Humans");
            DropTable("dbo.Errors");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.CompanyInformations");
            DropTable("dbo.SKUs");
            DropTable("dbo.CampaignSKUs");
            DropTable("dbo.Campaigns");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Bundles");
            DropTable("dbo.Accounts");
        }
    }
}
