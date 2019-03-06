namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTSCDCCDCTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TangGiamTSCDs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaTSCD = c.String(nullable: false, maxLength: 20, unicode: false),
                        Type = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 256),
                        NgaySuDung = c.DateTime(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        GiaTriBanDau = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiaTriConLai = c.Decimal(precision: 18, scale: 2),
                        ThoiGianSuDung = c.Int(nullable: false),
                        GiaTriPhanBoTrongKy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoPhan = c.String(nullable: false, maxLength: 10, unicode: false),
                        PhanBo = c.String(nullable: false, maxLength: 10, unicode: false),
                        ThangSuDung = c.Int(nullable: false),
                        NamSuDung = c.Int(nullable: false),
                        Note = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TimeTrichKhauHaoTSCDs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DanhMucNhomTSCD = c.String(nullable: false, maxLength: 256),
                        NhomTSCD = c.String(nullable: false, maxLength: 256),
                        TimeMix = c.Int(nullable: false),
                        TimeMax = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeTrichKhauHaoTSCDs");
            DropTable("dbo.TangGiamTSCDs");
        }
    }
}
