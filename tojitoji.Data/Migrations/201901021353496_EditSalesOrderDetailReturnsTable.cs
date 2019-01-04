namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSalesOrderDetailReturnsTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SalesOrderDetailReturns");
            AddColumn("dbo.SalesOrderDetailReturns", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SalesOrderDetailReturns", "ID");
            CreateIndex("dbo.SalesOrderDetailReturns", "ID");
            CreateIndex("dbo.SalesOrderDetailReturns", "DocumentTypeID");
            AddForeignKey("dbo.SalesOrderDetailReturns", "DocumentTypeID", "dbo.DocumentTypes", "ID");
            AddForeignKey("dbo.SalesOrderDetailReturns", "ID", "dbo.SalesOrderDetails", "ID");
            DropColumn("dbo.SalesOrderDetailReturns", "SalesOrderDetailID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrderDetailReturns", "SalesOrderDetailID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SalesOrderDetailReturns", "ID", "dbo.SalesOrderDetails");
            DropForeignKey("dbo.SalesOrderDetailReturns", "DocumentTypeID", "dbo.DocumentTypes");
            DropIndex("dbo.SalesOrderDetailReturns", new[] { "DocumentTypeID" });
            DropIndex("dbo.SalesOrderDetailReturns", new[] { "ID" });
            DropPrimaryKey("dbo.SalesOrderDetailReturns");
            DropColumn("dbo.SalesOrderDetailReturns", "ID");
            AddPrimaryKey("dbo.SalesOrderDetailReturns", "SalesOrderDetailID");
        }
    }
}
