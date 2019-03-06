namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTimeTrichKhauHaoTSCDTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeTrichKhauHaoTSCDs", "TimeMin", c => c.Int(nullable: false));
            DropColumn("dbo.TimeTrichKhauHaoTSCDs", "TimeMix");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeTrichKhauHaoTSCDs", "TimeMix", c => c.Int(nullable: false));
            DropColumn("dbo.TimeTrichKhauHaoTSCDs", "TimeMin");
        }
    }
}
