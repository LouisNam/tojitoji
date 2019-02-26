namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKhoTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Khos", "LoaiKhoID", "dbo.LoaiKhos");
            DropIndex("dbo.Khos", new[] { "LoaiKhoID" });
            DropColumn("dbo.Khos", "LoaiKhoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Khos", "LoaiKhoID", c => c.Int(nullable: false));
            CreateIndex("dbo.Khos", "LoaiKhoID");
            AddForeignKey("dbo.Khos", "LoaiKhoID", "dbo.LoaiKhos", "ID", cascadeDelete: true);
        }
    }
}
