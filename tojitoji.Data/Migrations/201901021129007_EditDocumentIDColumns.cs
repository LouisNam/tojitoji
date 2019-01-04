namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditDocumentIDColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "DocumentID", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PurchaseOrderDetailReturns", "DocumentID", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.PurchaseOrders", "DocumentID", c => c.String(maxLength: 20, unicode: false));
            DropColumn("dbo.PurchaseOrderDetailReturns", "MaChungTu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderDetailReturns", "MaChungTu", c => c.Int());
            AlterColumn("dbo.PurchaseOrders", "DocumentID", c => c.String());
            DropColumn("dbo.PurchaseOrderDetailReturns", "DocumentID");
            DropColumn("dbo.Documents", "DocumentID");
        }
    }
}
