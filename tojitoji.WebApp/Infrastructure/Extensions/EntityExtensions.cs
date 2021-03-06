﻿using tojitoji.Model.Models;
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
            companyInformation.Image = companyInformationVM.Image;
            companyInformation.Note = companyInformationVM.Note;
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
            humanType.Type_1 = humanTypeVM.Type_1;
            humanType.Type_2 = humanTypeVM.Type_2;
            humanType.Type_3 = humanTypeVM.Type_3;
        }

        public static void UpdateCategory(this Category category, CategoryViewModel categoryVM)
        {
            category.ID = categoryVM.ID;
            category.Code = categoryVM.Code;
            category.Categories = categoryVM.Categories;
            category.Categories_Type = categoryVM.Categories_Type;
            category.MacroCategories = categoryVM.MacroCategories;
            category.CommercialCate = categoryVM.CommercialCate;
            category.Name_1 = categoryVM.Name_1;
            category.Name_2 = categoryVM.Name_2;
            category.Name_3 = categoryVM.Name_3;
            category.Name_4 = categoryVM.Name_4;
            category.Name_5 = categoryVM.Name_5;
            category.Name_6 = categoryVM.Name_6;
            category.NameEn_1 = categoryVM.NameEn_1;
            category.NameEn_2 = categoryVM.NameEn_2;
            category.NameEn_3 = categoryVM.NameEn_3;
            category.NameEn_4 = categoryVM.NameEn_4;
            category.NameEn_5 = categoryVM.NameEn_5;
            category.NameEn_6 = categoryVM.NameEn_6;
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

        public static void UpdatePurchaseOrder(this PurchaseOrder purchaseOrder, PurchaseOrderViewModel purchaseOrderVM)
        {
            purchaseOrder.ID = purchaseOrderVM.ID;
            purchaseOrder.CreatedDate = purchaseOrderVM.CreatedDate;
            purchaseOrder.DocumentTypeID = purchaseOrderVM.DocumentTypeID;
            purchaseOrder.DocumentID = purchaseOrderVM.DocumentID;
            purchaseOrder.Description = purchaseOrderVM.Description;
            purchaseOrder.SupplierID = purchaseOrderVM.SupplierID;
            purchaseOrder.SubmittedByID = purchaseOrderVM.SubmittedByID;
            purchaseOrder.SubmittedDate = purchaseOrderVM.SubmittedDate;
            purchaseOrder.ApprovedByID = purchaseOrderVM.ApprovedByID;
            purchaseOrder.ApprovedDate = purchaseOrderVM.ApprovedDate;
            purchaseOrder.Note = purchaseOrderVM.Note;
            purchaseOrder.PurchasePlace = purchaseOrderVM.PurchasePlace;
        }

        public static void UpdatePurchaseOrderDetail(this PurchaseOrderDetail purchaseOrderDetail, PurchaseOrderDetailViewModel purchaseOrderDetailVM)
        {
            purchaseOrderDetail.ID = purchaseOrderDetailVM.ID;
            purchaseOrderDetail.ItemID = purchaseOrderDetailVM.ItemID;
            purchaseOrderDetail.ProductID = purchaseOrderDetailVM.ProductID;
            purchaseOrderDetail.PurchaseOrderID = purchaseOrderDetailVM.PurchaseOrderID;
            purchaseOrderDetail.Status = purchaseOrderDetailVM.Status;
            purchaseOrderDetail.PurchasingPrice = purchaseOrderDetailVM.PurchasingPrice;
            purchaseOrderDetail.DiscountPercent = purchaseOrderDetailVM.DiscountPercent;
            purchaseOrderDetail.DiscountAmount = purchaseOrderDetailVM.DiscountAmount;
            purchaseOrderDetail.DiscountReason = purchaseOrderDetailVM.DiscountReason;
            purchaseOrderDetail.ShippingFeeDistributor = purchaseOrderDetailVM.ShippingFeeDistributor;
            purchaseOrderDetail.ShippingFee = purchaseOrderDetailVM.ShippingFee;
            purchaseOrderDetail.Subsidize = purchaseOrderDetailVM.Subsidize;
            purchaseOrderDetail.UnitCost = purchaseOrderDetailVM.UnitCost;
            purchaseOrderDetail.StatusPayment = purchaseOrderDetailVM.StatusPayment;
            purchaseOrderDetail.DocumentNo = purchaseOrderDetailVM.DocumentNo;
            purchaseOrderDetail.PaymentMethod = purchaseOrderDetailVM.PaymentMethod;
            purchaseOrderDetail.CreatedDate = purchaseOrderDetailVM.CreatedDate;
            purchaseOrderDetail.UpdatedDate = purchaseOrderDetailVM.UpdatedDate;
            purchaseOrderDetail.ShippingTime = purchaseOrderDetailVM.ShippingTime;
            purchaseOrderDetail.CanceledTime = purchaseOrderDetailVM.CanceledTime;
            purchaseOrderDetail.DeliveriedETA = purchaseOrderDetailVM.DeliveriedETA;
            purchaseOrderDetail.DeliveriedTime = purchaseOrderDetailVM.DeliveriedTime;
            purchaseOrderDetail.FailedTime = purchaseOrderDetailVM.FailedTime;
            purchaseOrderDetail.PaidTime = purchaseOrderDetailVM.PaidTime;
            purchaseOrderDetail.ShippingParcel = purchaseOrderDetailVM.ShippingParcel;
            purchaseOrderDetail.TKN = purchaseOrderDetailVM.TKN;
            purchaseOrderDetail.TKC = purchaseOrderDetailVM.TKC;
        }

        public static void UpdatePurchaseOrderDetailReturn(this PurchaseOrderDetailReturn purchaseOrderDetailReturn, PurchaseOrderDetailReturnViewModel purchaseOrderDetailReturnVM)
        {
            purchaseOrderDetailReturn.ID = purchaseOrderDetailReturnVM.ID;
            purchaseOrderDetailReturn.ReturnedTime = purchaseOrderDetailReturnVM.ReturnedTime;
            purchaseOrderDetailReturn.ReturnedParcel = purchaseOrderDetailReturnVM.ReturnedParcel;
            purchaseOrderDetailReturn.ReturnedAmount = purchaseOrderDetailReturnVM.ReturnedAmount;
            purchaseOrderDetailReturn.ReturnedAmountTime = purchaseOrderDetailReturnVM.ReturnedAmountTime;
            purchaseOrderDetailReturn.PaymentMethod = purchaseOrderDetailReturnVM.PaymentMethod;
            purchaseOrderDetailReturn.TKN = purchaseOrderDetailReturnVM.TKN;
            purchaseOrderDetailReturn.TKC = purchaseOrderDetailReturnVM.TKC;
            purchaseOrderDetailReturn.StatusPayment = purchaseOrderDetailReturnVM.StatusPayment;
            purchaseOrderDetailReturn.DocumentNo = purchaseOrderDetailReturnVM.DocumentNo;
            purchaseOrderDetailReturn.PurchaseOrderReturnID = purchaseOrderDetailReturnVM.PurchaseOrderReturnID;
            purchaseOrderDetailReturn.CreatedDate = purchaseOrderDetailReturnVM.CreatedDate;
            purchaseOrderDetailReturn.DocumentTypeID = purchaseOrderDetailReturnVM.DocumentTypeID;
            purchaseOrderDetailReturn.DocumentID = purchaseOrderDetailReturnVM.DocumentID;
            purchaseOrderDetailReturn.Description = purchaseOrderDetailReturnVM.Description;
        }

        public static void UpdateSalesOrder(this SalesOrder salesOrder, SalesOrderViewModel salesOrderVM)
        {
            salesOrder.ID = salesOrderVM.ID;
            salesOrder.CreatedDate = salesOrderVM.CreatedDate;
            salesOrder.DocumentTypeID = salesOrderVM.DocumentTypeID;
            salesOrder.DocumentID = salesOrderVM.DocumentID;
            salesOrder.Description = salesOrderVM.Description;
            salesOrder.CustomerID = salesOrderVM.CustomerID;
            salesOrder.StaffID = salesOrderVM.StaffID;
            salesOrder.SalesPlace = salesOrderVM.SalesPlace;
            salesOrder.WarehouseID = salesOrderVM.WarehouseID;
            salesOrder.PartnerOrderID = salesOrderVM.PartnerOrderID;
        }

        public static void UpdateSalesOrderDetail(this SalesOrderDetail salesOrderDetail, SalesOrderDetailViewModel salesOrderDetailVM)
        {
            salesOrderDetail.ID = salesOrderDetailVM.ID;
            salesOrderDetail.SalesOrderID = salesOrderDetailVM.SalesOrderID;
            salesOrderDetail.ItemID = salesOrderDetailVM.ItemID;
            salesOrderDetail.SKU = salesOrderDetailVM.SKU;
            salesOrderDetail.Status = salesOrderDetailVM.Status;
            salesOrderDetail.SellingPrice = salesOrderDetailVM.SellingPrice;
            salesOrderDetail.DiscountPercent = salesOrderDetailVM.DiscountPercent;
            salesOrderDetail.DiscountAmount = salesOrderDetailVM.DiscountAmount;
            salesOrderDetail.DiscountReason = salesOrderDetailVM.DiscountReason;
            salesOrderDetail.ShippingFee = salesOrderDetailVM.ShippingFee;
            salesOrderDetail.ShippingFreeCustomer = salesOrderDetailVM.ShippingFreeCustomer;
            salesOrderDetail.OtherFee = salesOrderDetailVM.OtherFee;
            salesOrderDetail.CustomerPaid = salesOrderDetailVM.CustomerPaid;
            salesOrderDetail.Refund = salesOrderDetailVM.Refund;
            salesOrderDetail.StatusPayment = salesOrderDetailVM.StatusPayment;
            salesOrderDetail.PaymentMethod = salesOrderDetailVM.PaymentMethod;
            salesOrderDetail.PartnerOrderItemID = salesOrderDetailVM.PartnerOrderItemID;
            salesOrderDetail.CustomerComment = salesOrderDetailVM.CustomerComment;
            salesOrderDetail.CustomerRating = salesOrderDetailVM.CustomerRating;
            salesOrderDetail.Note = salesOrderDetailVM.Note;
            salesOrderDetail.UpdatedTime = salesOrderDetailVM.UpdatedTime;
            salesOrderDetail.CreatedTime = salesOrderDetailVM.CreatedTime;
            salesOrderDetail.ShippingTime = salesOrderDetailVM.ShippingTime;
            salesOrderDetail.CanceledTime = salesOrderDetailVM.CanceledTime;
            salesOrderDetail.DeliveriedETA = salesOrderDetailVM.DeliveriedETA;
            salesOrderDetail.DeliveriedTime = salesOrderDetailVM.DeliveriedTime;
            salesOrderDetail.FailedTime = salesOrderDetailVM.FailedTime;
            salesOrderDetail.PaidTime = salesOrderDetailVM.PaidTime;
            salesOrderDetail.BillingName = salesOrderDetailVM.BillingName;
            salesOrderDetail.BillingPhoneNumber1 = salesOrderDetailVM.BillingPhoneNumber1;
            salesOrderDetail.BillingPhoneNumber2 = salesOrderDetailVM.BillingPhoneNumber2;
            salesOrderDetail.BillingAddress = salesOrderDetailVM.BillingAddress;
            salesOrderDetail.BillingWard = salesOrderDetailVM.BillingWard;
            salesOrderDetail.BillingDistrict = salesOrderDetailVM.BillingDistrict;
            salesOrderDetail.BillingCity = salesOrderDetailVM.BillingCity;
            salesOrderDetail.BillingState = salesOrderDetailVM.BillingState;
            salesOrderDetail.BillingCountry = salesOrderDetailVM.BillingCountry;
            salesOrderDetail.BillingZIP = salesOrderDetailVM.BillingZIP;
            salesOrderDetail.ShippingName = salesOrderDetailVM.ShippingName;
            salesOrderDetail.ShippingPhoneNumber1 = salesOrderDetailVM.ShippingPhoneNumber1;
            salesOrderDetail.ShippingPhoneNumber2 = salesOrderDetailVM.ShippingPhoneNumber2;
            salesOrderDetail.ShippingAddress = salesOrderDetailVM.ShippingAddress;
            salesOrderDetail.ShippingWard = salesOrderDetailVM.ShippingWard;
            salesOrderDetail.ShippingDistrict = salesOrderDetailVM.ShippingDistrict;
            salesOrderDetail.ShippingState = salesOrderDetailVM.ShippingState;
            salesOrderDetail.ShippingCountry = salesOrderDetailVM.ShippingCountry;
            salesOrderDetail.ShippingZIP = salesOrderDetailVM.ShippingZIP;
            salesOrderDetail.TrackingCode = salesOrderDetailVM.TrackingCode;
            salesOrderDetail.TrackingURL = salesOrderDetailVM.TrackingURL;
            salesOrderDetail.ShippingProvider = salesOrderDetailVM.ShippingProvider;
            salesOrderDetail.ShippingMethod = salesOrderDetailVM.ShippingMethod;
            salesOrderDetail.ShippingParcel = salesOrderDetailVM.ShippingParcel;
            salesOrderDetail.TKNGiaVon = salesOrderDetailVM.TKNGiaVon;
            salesOrderDetail.TKCGiaVon = salesOrderDetailVM.TKCGiaVon;
            salesOrderDetail.TKNGiaBan = salesOrderDetailVM.TKNGiaBan;
            salesOrderDetail.TKCGiaBan = salesOrderDetailVM.TKCGiaBan;
            salesOrderDetail.DocumentNo = salesOrderDetailVM.DocumentNo;
        }

        public static void UpdateSalesOrderDetailReturn(this SalesOrderDetailReturn salesOrderDetailReturn, SalesOrderDetailReturnViewModel salesOrderDetailReturnVM)
        {
            salesOrderDetailReturn.ID = salesOrderDetailReturnVM.ID;
            salesOrderDetailReturn.ReturnedTime = salesOrderDetailReturnVM.ReturnedTime;
            salesOrderDetailReturn.Amount = salesOrderDetailReturnVM.Amount;
            salesOrderDetailReturn.AmountTime = salesOrderDetailReturnVM.AmountTime;
            salesOrderDetailReturn.Parcel = salesOrderDetailReturnVM.Parcel;
            salesOrderDetailReturn.TKNGiaVon = salesOrderDetailReturnVM.TKNGiaVon;
            salesOrderDetailReturn.TKCGiaVon = salesOrderDetailReturnVM.TKCGiaVon;
            salesOrderDetailReturn.TKNGiaBan = salesOrderDetailReturnVM.TKNGiaBan;
            salesOrderDetailReturn.TKCGiaBan = salesOrderDetailReturnVM.TKCGiaBan;
            salesOrderDetailReturn.DocumentNo = salesOrderDetailReturnVM.DocumentNo;
            salesOrderDetailReturn.SalesOrderReturnID = salesOrderDetailReturnVM.SalesOrderReturnID;
            salesOrderDetailReturn.CreatedDate = salesOrderDetailReturnVM.CreatedDate;
            salesOrderDetailReturn.DocumentTypeID = salesOrderDetailReturnVM.DocumentTypeID;
            salesOrderDetailReturn.DocumentID = salesOrderDetailReturnVM.DocumentID;
            salesOrderDetailReturn.Description = salesOrderDetailReturnVM.Description;
        }

        public static void UpdateDocument(this Document document, DocumentViewModel DocumentVM)
        {
            document.ID = DocumentVM.ID;
            document.DocumentTypeID = DocumentVM.DocumentTypeID;
            document.Date = DocumentVM.Date;
            document.Description = DocumentVM.Description;
            document.HumanID = DocumentVM.HumanID;
            document.Serial = DocumentVM.Serial;
            document.BillNo = DocumentVM.BillNo;
            document.BillDate = DocumentVM.BillDate;
        }

        public static void UpdateTransaction(this Transaction transaction, TransactionViewModel transactionVM)
        {
            transaction.ID = transactionVM.ID;
            transaction.Date = transactionVM.Date;
            transaction.Description = transactionVM.Description;
            transaction.Amount = transactionVM.Amount;
            transaction.DocumentID = transactionVM.DocumentID;
            transaction.HumanID = transactionVM.HumanID;
            transaction.TKN = transactionVM.TKN;
            transaction.TKC = transactionVM.TKC;
        }

        public static void UpdateTrialBalance(this TrialBalance trialBalance, TrialBalanceViewModel trialBalanceVM)
        {
            trialBalance.ID = trialBalanceVM.ID;
            trialBalance.DocumentTypeID = trialBalanceVM.DocumentTypeID;
            trialBalance.Date = trialBalanceVM.Date;
            trialBalance.DocumentID = trialBalanceVM.DocumentID;
            trialBalance.Description = trialBalanceVM.Description;
            trialBalance.HumanID = trialBalanceVM.HumanID;
            trialBalance.AccountID = trialBalanceVM.AccountID;
            trialBalance.CorrespondingAccountID = trialBalanceVM.CorrespondingAccountID;
            trialBalance.DebitIncurred = trialBalanceVM.DebitIncurred;
            trialBalance.CreditIncurred = trialBalanceVM.CreditIncurred;
        }

        public static void UpdateBible(this Bible bible, BibleViewModel bibleVM)
        {
            bible.ID = bibleVM.ID;
            bible.Shortcut = bibleVM.Shortcut;
            bible.Meaning = bibleVM.Meaning;
        }

        public static void UpdateCoSoKinhDoanh(this CoSoKinhDoanh coSoKinhDoanh, CoSoKinhDoanhViewModel coSoKinhDoanhVM)
        {
            coSoKinhDoanh.ID = coSoKinhDoanhVM.ID;
            coSoKinhDoanh.Place_1 = coSoKinhDoanhVM.Place_1;
            coSoKinhDoanh.Place_2 = coSoKinhDoanhVM.Place_2;
            coSoKinhDoanh.Place_3 = coSoKinhDoanhVM.Place_3;
            coSoKinhDoanh.Place_4 = coSoKinhDoanhVM.Place_4;
            coSoKinhDoanh.Status = coSoKinhDoanhVM.Status;
            coSoKinhDoanh.Note = coSoKinhDoanhVM.Note;
        }

        public static void UpdateLoaiTaiSan(this LoaiTaiSan loaiTaiSan, LoaiTaiSanViewModel loaiTaiSanVM)
        {
            loaiTaiSan.ID = loaiTaiSanVM.ID;
            loaiTaiSan.Name = loaiTaiSanVM.Name;
        }

        public static void UpdateTaiSan(this TaiSan taiSan, TaiSanViewModel taiSanVM)
        {
            taiSan.ID = taiSanVM.ID;
            taiSan.Name = taiSanVM.Name;
            taiSan.LoaiTaiSanID = taiSanVM.LoaiTaiSanID;
        }

        public static void UpdateLoaiKho(this LoaiKho loaiKho, LoaiKhoViewModel loaiKhoVM)
        {
            loaiKho.ID = loaiKhoVM.ID;
            loaiKho.Name = loaiKhoVM.Name;
            loaiKho.Description = loaiKhoVM.Description;
        }

        public static void UpdateKho(this Kho kho, KhoViewModel khoVM)
        {
            kho.ID = khoVM.ID;
            kho.Kho_1 = khoVM.Kho_1;
            kho.Kho_2 = khoVM.Kho_2;
            kho.Kho_3 = khoVM.Kho_3;
            kho.Kho_4 = khoVM.Kho_4;
            kho.Status = khoVM.Status;
            kho.Note = khoVM.Note;
        }

        public static void UpdateTimeTrichKhauHaoTSCD(this TimeTrichKhauHaoTSCD timeTrichKhauHaoTSCD, TimeTrichKhauHaoTSCDViewModel timeTrichKhauHaoTSCDVM)
        {
            timeTrichKhauHaoTSCD.ID = timeTrichKhauHaoTSCDVM.ID;
            timeTrichKhauHaoTSCD.DanhMucNhomTSCD = timeTrichKhauHaoTSCDVM.DanhMucNhomTSCD;
            timeTrichKhauHaoTSCD.NhomTSCD = timeTrichKhauHaoTSCDVM.NhomTSCD;
            timeTrichKhauHaoTSCD.TimeMin = timeTrichKhauHaoTSCDVM.TimeMin;
            timeTrichKhauHaoTSCD.TimeMax = timeTrichKhauHaoTSCDVM.TimeMax;
        }

        public static void UpdateTangGiamTSCD (this TangGiamTSCD tangGiamTSCD, TangGiamTSCDViewModel tangGiamTSCDVM)
        {
            tangGiamTSCD.ID = tangGiamTSCDVM.ID;
            tangGiamTSCD.MaTSCD = tangGiamTSCDVM.MaTSCD;
            tangGiamTSCD.Type = tangGiamTSCDVM.Type;
            tangGiamTSCD.Name = tangGiamTSCDVM.Name;
            tangGiamTSCD.NgaySuDung = tangGiamTSCDVM.NgaySuDung;
            tangGiamTSCD.SoLuong = tangGiamTSCDVM.SoLuong;
            tangGiamTSCD.GiaTriBanDau = tangGiamTSCDVM.GiaTriBanDau;
            tangGiamTSCD.GiaTriConLai = tangGiamTSCDVM.GiaTriConLai;
            tangGiamTSCD.ThoiGianSuDung = tangGiamTSCDVM.ThoiGianSuDung;
            tangGiamTSCD.GiaTriPhanBoTrongKy = tangGiamTSCDVM.GiaTriPhanBoTrongKy;
            tangGiamTSCD.BoPhan = tangGiamTSCDVM.BoPhan;
            tangGiamTSCD.PhanBo = tangGiamTSCDVM.PhanBo;
            tangGiamTSCD.ThangSuDung = tangGiamTSCDVM.ThangSuDung;
            tangGiamTSCD.NamSuDung = tangGiamTSCDVM.NamSuDung;
            tangGiamTSCD.Note = tangGiamTSCDVM.Note;
        }
    }
}