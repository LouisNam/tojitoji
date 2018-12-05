using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("CompanyInformations")]
    public class CompanyInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string CompanyName { set; get; }
        public string ShortName { set; get; }
        public string SoHuuVonType { set; get; }
        public string Address { set; get; }
        public string MaSoThue { set; get; }
        public DateTime MSTDate { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Email { set; get; }
        public string BankAccount { set; get; }
        public string CEO { set; get; }
        public string ChiefAccountant { set; get; }
        public string NguoiLapBieu { set; get; }
        public string Cashier { set; get; }
        public string CheDoKeToanApDung { set; get; }
        public string HinhThucKeToan { set; get; }
        public string PPThueGTGT { set; get; }
        public string PPKhauHao { set; get; }
        public string PPTinhGia { set; get; }
        public string PPHachToanTonKho { set; get; }
        public string PPTinhGiaTonKho { set; get; }
        public string VonDieuLe { set; get; }
        public string ThueSuat { set; get; }
        public string FinancialYear { set; get; }
        public string Website { set; get; }
        public string Fanpage { set; get; }
        public string Youtube { set; get; }
        public string Group { set; get; }
    }
}