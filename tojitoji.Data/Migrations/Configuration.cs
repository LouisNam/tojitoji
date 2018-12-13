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
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(tojitoji.Data.tojitojiDbContext context)
        {
            CreateCompanyInformationSample(context);
            CreateHumanTypeSample(context);
            CreateHumanSample(context);
            CreateCategorySample(context);
            CreateProductSample(context);
            CreateBundleSample(context);
            CreateSKUSample(context);
            CreateCampaignSKUSample(context);
            CreateCampaignSample(context);
            CreateWarehouseSample(context);
            CreateInventoryTransactionSample(context);
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

        private void CreateCategorySample(tojitojiDbContext context)
        {
            if (context.Categories.Count() == 0)
            {
                List<Category> listCategories = new List<Category>()
            {
                new Category() { CategoryType = 1, MacroCategory = "FMCG", Name = "Bách hóa Online", NameEn = "Groceries" },
                new Category() { CategoryType = 1, MacroCategory = "FMCG", Name = "Chăm sóc sức khỏe & Làm đẹp", NameEn = "Health & Beauty" },
                new Category() { CategoryType = 2, MacroCategory = "Home", Name = "Bếp & Phòng ăn", NameEn = "Kitchen & Dining" },
            };
                context.Categories.AddRange(listCategories);
                context.SaveChanges();
            }
        }

        private void CreateProductSample(tojitojiDbContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> listProducts = new List<Product>()
            {
                new Product() { Name = "Sản phẩm 1", RRP = 10000, SP = 12000, SpecialFromTime = DateTime.Now, SpecialToTime=DateTime.Now, Status=true, CategoryID=1,ProductLifeTime=24,Warranty=24,PackageWeight=1,PackageLength=1,PackageWidth=1,PackageHeight=1 },
                new Product() { Name = "Sản phẩm 2", RRP = 12000, SP = 13000, SpecialFromTime = DateTime.Now, SpecialToTime=DateTime.Now, Status=true, CategoryID=1,ProductLifeTime=24,Warranty=24,PackageWeight=1,PackageLength=1,PackageWidth=1,PackageHeight=1 },
                new Product() { Name = "Sản phẩm 3", RRP = 14000, SP = 11000, SpecialFromTime = DateTime.Now, SpecialToTime=DateTime.Now, Status=true, CategoryID=2,ProductLifeTime=24,Warranty=24,PackageWeight=1,PackageLength=1,PackageWidth=1,PackageHeight=1 },
            };
                context.Products.AddRange(listProducts);
                context.SaveChanges();
            }
        }

        private void CreateBundleSample(tojitojiDbContext context)
        {
            if (context.Bundles.Count() == 0)
            {
                List<Bundle> listCategories = new List<Bundle>()
            {
                new Bundle() { BundleType = "Buy 1 get 1", SKUBundle = "B1", BundleName = "Bánh mì 1 tặng bánh mì 2", ProductID = 1, ProductQuantity = 2, ProductNo = 1},
                new Bundle() { BundleType = "Buy 1 get 1", SKUBundle = "B1", BundleName = "Bánh mì 1 tặng bánh mì 2", ProductID = 2, ProductQuantity = 2, ProductNo = 2},
                new Bundle() { BundleType = "Combo", SKUBundle = "B2", BundleName = "2 bánh mì 1", ProductID = 3, ProductQuantity = 1},
            };
                context.Bundles.AddRange(listCategories);
                context.SaveChanges();
            }
        }

        private void CreateSKUSample(tojitojiDbContext context)
        {
            if (context.SKUs.Count() == 0)
            {
                List<SKU> listItem = new List<SKU>()
            {
                new SKU() { ProductID = 1, BundleID = 1 },
                new SKU() { ProductID = 2, BundleID = 2 },
                new SKU() { ProductID = 1, BundleID = 3 },
            };
                context.SKUs.AddRange(listItem);
                context.SaveChanges();
            }
        }

        private void CreateCampaignSKUSample(tojitojiDbContext context)
        {
            if (context.CampaignSKUs.Count() == 0)
            {
                List<CampaignSKU> listItem = new List<CampaignSKU>()
            {
                new CampaignSKU() { SKUID = 1, Price = 10000 },
                new CampaignSKU() { SKUID = 2, Price = 10000 },
                new CampaignSKU() { SKUID = 3, Price = 15000 },
            };
                context.CampaignSKUs.AddRange(listItem);
                context.SaveChanges();
            }
        }

        private void CreateCampaignSample(tojitojiDbContext context)
        {
            if (context.Campaigns.Count() == 0)
            {
                List<Campaign> listItem = new List<Campaign>()
            {
                new Campaign() { CampaignID = 1, Name = "Giảm giá", FromTime = DateTime.Now, ToTime = DateTime.Now },
                new Campaign() { CampaignID = 3, Name = "Giảm giá", FromTime = DateTime.Now, ToTime = DateTime.Now }
            };
                context.Campaigns.AddRange(listItem);
                context.SaveChanges();
            }
        }

        private void CreateWarehouseSample(tojitojiDbContext context)
        {
            if (context.Warehouses.Count() == 0)
            {
                List<Warehouse> listItem = new List<Warehouse>()
            {
                new Warehouse() { Name = "Kho 1", Status = true, Note = null },
                new Warehouse() { Name = "Kho 2", Status = true, Note = null },
                new Warehouse() { Name = "Kho 3", Status = false, Note = null, ParentID = 2 },
                new Warehouse() { Name = "Kho 4", Status = false, Note = null, ParentID = 3 },

            };
                context.Warehouses.AddRange(listItem);
                context.SaveChanges();
            }
        }

        private void CreateInventoryTransactionSample(tojitojiDbContext context)
        {
            if (context.InventoryTransactions.Count() == 0)
            {
                List<InventoryTransaction> listItem = new List<InventoryTransaction>()
            {
                new InventoryTransaction() { ModifiedDate = DateTime.Now, CreatedDate = DateTime.Now, Type = true, Status = true },
                new InventoryTransaction() { ModifiedDate = DateTime.Now, CreatedDate = DateTime.Now, Type = false, Status = false },

            };
                context.InventoryTransactions.AddRange(listItem);
                context.SaveChanges();
            }
        }
    }
}