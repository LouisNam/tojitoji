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
                        ID = c.String(nullable: false, maxLength: 128),
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
            DropForeignKey("dbo.Humans", "TypeCode", "dbo.HumanTypes");
            DropForeignKey("dbo.Campaigns", "CampaignID", "dbo.CampaignSKUs");
            DropForeignKey("dbo.CampaignSKUs", "SKUID", "dbo.SKUs");
            DropForeignKey("dbo.SKUs", "ProductID", "dbo.Products");
            DropForeignKey("dbo.SKUs", "BundleID", "dbo.Bundles");
            DropForeignKey("dbo.Bundles", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SKUsLazada", new[] { "SKUID" });
            DropIndex("dbo.Humans", new[] { "TypeCode" });
            DropIndex("dbo.SKUs", new[] { "BundleID" });
            DropIndex("dbo.SKUs", new[] { "ProductID" });
            DropIndex("dbo.CampaignSKUs", new[] { "SKUID" });
            DropIndex("dbo.Campaigns", new[] { "CampaignID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Bundles", new[] { "ProductID" });
            DropTable("dbo.SKUsLazada");
            DropTable("dbo.HumanTypes");
            DropTable("dbo.Humans");
            DropTable("dbo.Errors");
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
