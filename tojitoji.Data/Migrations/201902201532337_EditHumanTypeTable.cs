namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditHumanTypeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HumanTypes", "Type_1", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.HumanTypes", "Type_2", c => c.String(maxLength: 50));
            AddColumn("dbo.HumanTypes", "Type_3", c => c.String(maxLength: 50));
            DropColumn("dbo.HumanTypes", "Type");
            DropColumn("dbo.HumanTypes", "ParentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HumanTypes", "ParentID", c => c.Int());
            AddColumn("dbo.HumanTypes", "Type", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.HumanTypes", "Type_3");
            DropColumn("dbo.HumanTypes", "Type_2");
            DropColumn("dbo.HumanTypes", "Type_1");
        }
    }
}
