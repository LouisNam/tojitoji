namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearDBAndAddAccountTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.PostTags", new[] { "PostID" });
            DropIndex("dbo.PostTags", new[] { "TagID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.ProductTags", new[] { "TagID" });
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
            
            DropTable("dbo.PostCategories");
            DropTable("dbo.Posts");
            DropTable("dbo.PostTags");
            DropTable("dbo.Tags");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.ProductTags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.TagID });
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        CategoryID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        MoreImages = c.String(storeType: "xml"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PromotionPrice = c.Decimal(precision: 18, scale: 2),
                        Warranty = c.Int(),
                        Description = c.String(maxLength: 500),
                        Content = c.String(),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        Tags = c.String(),
                        Quantity = c.Int(nullable: false),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        PostID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.TagID });
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        CategoryID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        Content = c.String(),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Accounts");
            CreateIndex("dbo.ProductTags", "TagID");
            CreateIndex("dbo.ProductTags", "ProductID");
            CreateIndex("dbo.Products", "CategoryID");
            CreateIndex("dbo.PostTags", "TagID");
            CreateIndex("dbo.PostTags", "PostID");
            CreateIndex("dbo.Posts", "CategoryID");
            AddForeignKey("dbo.ProductTags", "TagID", "dbo.Tags", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductTags", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PostTags", "TagID", "dbo.Tags", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PostTags", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories", "ID", cascadeDelete: true);
        }
    }
}
