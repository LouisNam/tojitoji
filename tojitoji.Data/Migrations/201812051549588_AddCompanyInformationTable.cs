namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyInformationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyInformations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        ShortName = c.String(),
                        SoHuuVonType = c.String(),
                        Address = c.String(),
                        MaSoThue = c.String(),
                        MSTDate = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        BankAccount = c.String(),
                        CEO = c.String(),
                        ChiefAccountant = c.String(),
                        NguoiLapBieu = c.String(),
                        Cashier = c.String(),
                        CheDoKeToanApDung = c.String(),
                        HinhThucKeToan = c.String(),
                        PPThueGTGT = c.String(),
                        PPKhauHao = c.String(),
                        PPTinhGia = c.String(),
                        PPHachToanTonKho = c.String(),
                        PPTinhGiaTonKho = c.String(),
                        VonDieuLe = c.String(),
                        ThueSuat = c.String(),
                        FinancialYear = c.String(),
                        Website = c.String(),
                        Fanpage = c.String(),
                        Youtube = c.String(),
                        Group = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompanyInformations");
        }
    }
}
