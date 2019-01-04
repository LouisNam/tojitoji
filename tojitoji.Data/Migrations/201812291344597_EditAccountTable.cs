namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAccountTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "SoDuNoDau", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Accounts", "SoDuCoDau", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Accounts", "CreatedDate");
            DropColumn("dbo.Accounts", "CreatedBy");
            DropColumn("dbo.Accounts", "UpdatedDate");
            DropColumn("dbo.Accounts", "UpdatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Accounts", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Accounts", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Accounts", "CreatedDate", c => c.DateTime());
            DropColumn("dbo.Accounts", "SoDuCoDau");
            DropColumn("dbo.Accounts", "SoDuNoDau");
        }
    }
}
