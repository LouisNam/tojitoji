namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCampainTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CampaignSKUs", "SKUID", "dbo.SKUs");
            DropForeignKey("dbo.Campaigns", "CampaignID", "dbo.CampaignSKUs");
            DropIndex("dbo.Campaigns", new[] { "CampaignID" });
            DropIndex("dbo.CampaignSKUs", new[] { "SKUID" });
            DropTable("dbo.Campaigns");
            DropTable("dbo.CampaignSKUs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CampaignSKUs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SKUID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .PrimaryKey(t => t.CampaignID);
            
            CreateIndex("dbo.CampaignSKUs", "SKUID");
            CreateIndex("dbo.Campaigns", "CampaignID");
            AddForeignKey("dbo.Campaigns", "CampaignID", "dbo.CampaignSKUs", "ID");
            AddForeignKey("dbo.CampaignSKUs", "SKUID", "dbo.SKUs", "ID", cascadeDelete: true);
        }
    }
}
