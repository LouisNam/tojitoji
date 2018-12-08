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
    }
}