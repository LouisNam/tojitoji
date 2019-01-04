namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAccountTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "NgaySoDu", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "NgaySoDu");
        }
    }
}
