namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        AccountType = c.Boolean(),
                        Account_Name = c.String(nullable: false, maxLength: 256),
                        Account_1 = c.Int(nullable: false),
                        Account_1_Name = c.String(maxLength: 256),
                        Account_2 = c.Int(),
                        Account_2_Name = c.String(maxLength: 256),
                        Account_3 = c.Int(),
                        Account_3_Name = c.String(maxLength: 256),
                        Status = c.Boolean(),
                        TKNhanKC = c.String(),
                        MaKC = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Humans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        TypeCode = c.Int(nullable: false),
                        Company = c.String(maxLength: 50),
                        Gender = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        District = c.String(maxLength: 50),
                        Ward = c.String(maxLength: 50),
                        OtherContact = c.String(maxLength: 256),
                        TaxCode = c.Int(),
                        Picture = c.String(maxLength: 256),
                        Note = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateOfEntry = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HumanTypes", t => t.TypeCode, cascadeDelete: true)
                .Index(t => t.TypeCode);
            
            CreateTable(
                "dbo.HumanTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Humans", "TypeCode", "dbo.HumanTypes");
            DropIndex("dbo.Humans", new[] { "TypeCode" });
            DropTable("dbo.HumanTypes");
            DropTable("dbo.Humans");
            DropTable("dbo.Errors");
            DropTable("dbo.CompanyInformations");
            DropTable("dbo.Accounts");
        }
    }
}
