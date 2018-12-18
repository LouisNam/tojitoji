namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductIDToInventoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryTransactions", "ProductID", c => c.Int());
            CreateIndex("dbo.InventoryTransactions", "ProductID");
            AddForeignKey("dbo.InventoryTransactions", "ProductID", "dbo.Products", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryTransactions", "ProductID", "dbo.Products");
            DropIndex("dbo.InventoryTransactions", new[] { "ProductID" });
            DropColumn("dbo.InventoryTransactions", "ProductID");
        }
    }
}
