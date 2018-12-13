using tojitoji.Model.Models;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateCompanyInformation(this CompanyInformation companyInformation, CompanyInformationViewModel companyInformationVM)
        {
            companyInformation.ID = companyInformationVM.ID;
            companyInformation.CompanyName = companyInformationVM.CompanyName;
            companyInformation.ShortName = companyInformationVM.ShortName;
            companyInformation.SoHuuVonType = companyInformationVM.SoHuuVonType;
            companyInformation.Address = companyInformationVM.Address;
            companyInformation.MaSoThue = companyInformationVM.MaSoThue;
            companyInformation.MSTDate = companyInformationVM.MSTDate;
            companyInformation.Phone = companyInformationVM.Phone;
            companyInformation.Fax = companyInformationVM.Fax;
            companyInformation.Email = companyInformationVM.Email;
            companyInformation.BankAccount = companyInformationVM.BankAccount;
            companyInformation.CEO = companyInformationVM.CEO;
            companyInformation.ChiefAccountant = companyInformationVM.ChiefAccountant;
            companyInformation.NguoiLapBieu = companyInformationVM.NguoiLapBieu;
            companyInformation.Cashier = companyInformationVM.Cashier;
            companyInformation.CheDoKeToanApDung = companyInformationVM.CheDoKeToanApDung;
            companyInformation.HinhThucKeToan = companyInformationVM.HinhThucKeToan;
            companyInformation.PPThueGTGT = companyInformationVM.PPThueGTGT;
            companyInformation.PPKhauHao = companyInformationVM.PPKhauHao;
            companyInformation.PPTinhGia = companyInformationVM.PPTinhGia;
            companyInformation.PPHachToanTonKho = companyInformationVM.PPHachToanTonKho;
            companyInformation.PPTinhGiaTonKho = companyInformationVM.PPTinhGiaTonKho;
            companyInformation.VonDieuLe = companyInformationVM.VonDieuLe;
            companyInformation.ThueSuat = companyInformationVM.ThueSuat;
            companyInformation.FinancialYear = companyInformationVM.FinancialYear;
            companyInformation.Website = companyInformationVM.Website;
            companyInformation.Fanpage = companyInformationVM.Fanpage;
            companyInformation.Youtube = companyInformationVM.Youtube;
            companyInformation.Group = companyInformationVM.Group;
        }

        public static void UpdateHuman(this Human human, HumanViewModel humanVM)
        {
            human.ID = humanVM.ID;
            human.FirstName = humanVM.FirstName;
            human.LastName = humanVM.LastName;
            human.TypeCode = humanVM.TypeCode;
            human.Company = humanVM.Company;
            human.Gender = humanVM.Gender;
            human.Phone = humanVM.Phone;
            human.Email = humanVM.Email;
            human.JobTitle = humanVM.JobTitle;
            human.Address = humanVM.Address;
            human.Province = humanVM.Province;
            human.City = humanVM.City;
            human.District = humanVM.District;
            human.Ward = humanVM.Ward;
            human.OtherContact = humanVM.OtherContact;
            human.TaxCode = humanVM.TaxCode;
            human.Picture = humanVM.Picture;
            human.Note = humanVM.Note;
            human.DateOfBirth = humanVM.DateOfBirth;
            human.DateOfEntry = humanVM.DateOfEntry;
        }

        public static void UpdateHumanType(this HumanType humanType, HumanTypeViewModel humanTypeVM)
        {
            humanType.ID = humanTypeVM.ID;
            humanType.Type = humanTypeVM.Type;
            humanType.ParentID = humanTypeVM.ParentID;
        }

        public static void UpdateCategory(this Category category, CategoryViewModel categoryVM)
        {
            category.ID = categoryVM.ID;
            category.CategoryType = categoryVM.CategoryType;
            category.MacroCategory = categoryVM.MacroCategory;
            category.CommercialCate = categoryVM.CommercialCate;
            category.Name = categoryVM.Name;
            category.NameEn = categoryVM.NameEn;
            category.ParentID = categoryVM.ParentID;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productVM)
        {
            product.ID = productVM.ID;
            product.Name = productVM.Name;
            product.RRP = productVM.RRP;
            product.SP = productVM.SP;
            product.SpecialFromTime = productVM.SpecialFromTime;
            product.SpecialToTime = productVM.SpecialToTime;
            product.Status = productVM.Status;
            product.NameEn = productVM.NameEn;
            product.CategoryID = productVM.CategoryID;
            product.Brand = productVM.Brand;
            product.Model = productVM.Model;
            product.ProductCode = productVM.ProductCode;
            product.ColorFamily = productVM.ColorFamily;
            product.Size = productVM.Size;
            product.ProductLifeTime = productVM.ProductLifeTime;
            product.Warranty = productVM.Warranty;
            product.WarrantyType = productVM.WarrantyType;
            product.Unit = productVM.Unit;
            product.PackageContent = productVM.PackageContent;
            product.PackageWeight = productVM.PackageWeight;
            product.PackageLength = productVM.PackageLength;
            product.PackageWidth = productVM.PackageWidth;
            product.PackageHeight = productVM.PackageHeight;
            product.ShortDescription = productVM.ShortDescription;
            product.Description = productVM.Description;
            product.Origin = productVM.Origin;
            product.Video = productVM.Video;
            product.MainImage = productVM.MainImage;
            product.MoreImage = productVM.MoreImage;
            product.Note = productVM.Note;
        }

        public static void UpdateBundle(this Bundle bundle, BundleViewModel bundleVM)
        {
            bundle.ID = bundleVM.ID;
            bundle.BundleType = bundleVM.BundleType;
            bundle.SKUBundle = bundleVM.SKUBundle;
            bundle.BundleName = bundleVM.BundleName;
            bundle.ProductID = bundleVM.ProductID;
            bundle.ProductQuantity = bundleVM.ProductQuantity;
            bundle.ProductNo = bundleVM.ProductNo;
            bundle.DiscountRate = bundleVM.DiscountRate;
            bundle.SpecialFromTime = bundleVM.SpecialFromTime;
            bundle.SpecialToTime = bundleVM.SpecialToTime;
        }

        public static void UpdateSKU(this SKU sKU, SKUViewModel sKUVM)
        {
            sKU.ID = sKUVM.ID;
            sKU.ProductID = sKUVM.ProductID;
            sKU.BundleID = sKUVM.BundleID;
        }

        public static void UpdateSKULazada(this SKULazada sKULazada, SKULazadaViewModel sKUlazadaVM)
        {
            sKULazada.ID = sKUlazadaVM.ID;
            sKULazada.Lazada_SKU = sKUlazadaVM.Lazada_SKU;
            sKULazada.SKUName = sKUlazadaVM.SKUName;
            sKULazada.SKUID = sKUlazadaVM.SKUID;
            sKULazada.Link = sKUlazadaVM.Link;
            sKULazada.Status = sKUlazadaVM.Status;
        }

        public static void UpdateCampaignSKU(this CampaignSKU campaignSKU, CampaignSKUViewModel campaignSKUVM)
        {
            campaignSKU.ID = campaignSKUVM.ID;
            campaignSKU.SKUID = campaignSKUVM.SKUID;
            campaignSKU.Price = campaignSKUVM.Price;
        }

        public static void UpdateCampaign(this Campaign campaign, CampaignViewModel campaignVM)
        {
            campaign.CampaignID = campaignVM.CampaignID;
            campaign.Name = campaignVM.Name;
            campaign.FromTime = campaignVM.FromTime;
            campaign.ToTime = campaignVM.ToTime;
        }

        public static void UpdateWarehouse(this Warehouse warehouse, WarehouseViewModel warehouseVM)
        {
            warehouse.ID = warehouseVM.ID;
            warehouse.Name = warehouseVM.Name;
            warehouse.Status = warehouseVM.Status;
            warehouse.Note = warehouseVM.Note;
            warehouse.ParentID = warehouseVM.ParentID;
        }

        public static void UpdateInventoryTransaction(this InventoryTransaction inventoryTransaction, InventoryTransactionViewModel inventoryTransactionVM)
        {
            inventoryTransaction.ID = inventoryTransactionVM.ID;
            inventoryTransaction.ModifiedDate = inventoryTransactionVM.ModifiedDate;
            inventoryTransaction.CreatedDate = inventoryTransactionVM.CreatedDate;
            inventoryTransaction.Type = inventoryTransactionVM.Type;
            inventoryTransaction.ItemID = inventoryTransactionVM.ItemID;
            inventoryTransaction.ParcelID = inventoryTransactionVM.ParcelID;
            inventoryTransaction.WarehouseID = inventoryTransactionVM.WarehouseID;
            inventoryTransaction.Status = inventoryTransactionVM.Status;
            inventoryTransaction.Note = inventoryTransactionVM.Note;
        }
    }
}