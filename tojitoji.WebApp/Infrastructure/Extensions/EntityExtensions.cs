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
    }
}