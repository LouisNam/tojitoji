namespace tojitoji.Data.Migrations
{
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<tojitoji.Data.tojitojiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(tojitoji.Data.tojitojiDbContext context)
        {
            CreateCompanyInformationSample(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        private void CreateCompanyInformationSample(tojitojiDbContext context)
        {
            if (context.CompanyInformations.Count() == 0)
            {
                List<CompanyInformation> listCompanyInformations = new List<CompanyInformation>()
            {
                new CompanyInformation() {
                        CompanyName = "tojitoji",
                        ShortName = "toji",
                        SoHuuVonType = "Sở hữu vốn",
                        Address = "1 Lê Duẩn, Quận 1, TP.HCM",
                        MaSoThue = "1234567891",
                        MSTDate = DateTime.Now,
                        Phone = "0918112771",
                        Fax = "0918112771",
                        Email = "namkute@gmail.com",
                        BankAccount = "321654987231",
                        CEO = "Nam kute",
                        ChiefAccountant = "Name handsome",
                        NguoiLapBieu = "Name smart",
                        Cashier = "Nam clever",
                        CheDoKeToanApDung = "Chế độ",
                        HinhThucKeToan = "Hình thức",
                        PPThueGTGT = "Thuế GTGT",
                        PPKhauHao = "Hấu khao",
                        PPTinhGia = "Giá",
                        PPHachToanTonKho = "Tồn kho",
                        PPTinhGiaTonKho = "Giá tồn kho",
                        VonDieuLe = "Vốn điều lệ",
                        ThueSuat = "Thuế suất",
                        FinancialYear = "31/12",
                        Website = "tojitoji.com",
                        Fanpage = "fb.com/tojitoji",
                        Youtube = "youtube.com/tojitoji",
                        Group = "abx.com",
                }
            };
                context.CompanyInformations.AddRange(listCompanyInformations);
                context.SaveChanges();
            }
        }
    }
}