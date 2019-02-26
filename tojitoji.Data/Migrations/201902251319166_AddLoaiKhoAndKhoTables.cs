namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoaiKhoAndKhoTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Khos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Kho_1 = c.String(maxLength: 256),
                        Kho_2 = c.String(maxLength: 256),
                        Kho_3 = c.String(maxLength: 256),
                        Kho_4 = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                        Note = c.String(maxLength: 256),
                        LoaiKhoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LoaiKhos", t => t.LoaiKhoID, cascadeDelete: true)
                .Index(t => t.LoaiKhoID);
            
            CreateTable(
                "dbo.LoaiKhos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10, unicode: false),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Khos", "LoaiKhoID", "dbo.LoaiKhos");
            DropIndex("dbo.Khos", new[] { "LoaiKhoID" });
            DropTable("dbo.LoaiKhos");
            DropTable("dbo.Khos");
        }
    }
}
