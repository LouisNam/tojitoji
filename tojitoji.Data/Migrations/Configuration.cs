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
            CreateHumanTypeSample(context);
            CreateHumanSample(context);
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

        private void CreateHumanTypeSample(tojitojiDbContext context)
        {
            if (context.HumanTypes.Count() == 0)
            {
                List<HumanType> listHumanTypes = new List<HumanType>()
            {
                new HumanType() { Type = "Khách hàng" },
                new HumanType() { Type = "Sỉ", ParentID = 1 },
                new HumanType() { Type = "Lẻ", ParentID = 1 },
                new HumanType() { Type = "Nhà cung cấp" },
                new HumanType() { Type = "Nhân viên" },
                new HumanType() { Type = "Full-Time", ParentID  = 5 },
                new HumanType() { Type = "Part-Time", ParentID  = 5 },
            };
                context.HumanTypes.AddRange(listHumanTypes);
                context.SaveChanges();
            }
        }

        private void CreateHumanSample(tojitojiDbContext context)
        {
            if (context.Humans.Count() == 0)
            {
                List<Human> listHumans = new List<Human>()
            {
                new Human() { FirstName = "Nam", LastName = "Do", TypeCode = 1, Gender = "Nam", Phone = "0918112771", Email = "donam@gmail.com" },
                new Human() { FirstName = "Khanh", LastName = "M", TypeCode = 2, Gender = "Nữ", Phone = "0918112772", Email = "khanh@gmail.com" },
                new Human() { FirstName = "Minh", LastName = "A", TypeCode = 3, Gender = "Nam", Phone = "0918112773", Email = "minhminh@gmail.com" },
            };
                context.Humans.AddRange(listHumans);
                context.SaveChanges();
            }
        }
    }
}