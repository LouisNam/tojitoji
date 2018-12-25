using System;
using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class SalesOrderDetailReturnViewModel
    {
        public int SalesOrderDetailID { set; get; }

        public DateTime ReturnedTime { set; get; }

        public decimal Amount { set; get; }

        public DateTime? AmountTime { set; get; }

        public string Parcel { set; get; }

        public string TKNGiaVon { set; get; }

        public string TKCGiaVon { set; get; }

        public string TKNGiaBan { set; get; }

        public string TKCGiaBan { set; get; }

        public int? DocumentNo { set; get; }

        public int SalesOrderReturnID { set; get; }

        public DateTime CreatedDate { set; get; }

        public string DocumentTypeID { set; get; }

        [MaxLength(20)]
        public string DocumentID { set; get; }

        public string Description { set; get; }
    }
}