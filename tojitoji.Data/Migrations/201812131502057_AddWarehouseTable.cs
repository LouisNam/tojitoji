namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarehouseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryTransactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Type = c.Boolean(nullable: false),
                        ItemID = c.Int(),
                        ParcelID = c.Int(),
                        WarehouseID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseID)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryTransactions", "WarehouseID", "dbo.Warehouses");
            DropIndex("dbo.InventoryTransactions", new[] { "WarehouseID" });
            DropTable("dbo.Warehouses");
            DropTable("dbo.InventoryTransactions");
        }
    }
}
