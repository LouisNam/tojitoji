using System;

namespace tojitoji.WebApp.Models
{
    public class PurchaseOrderDetailReturnViewModel
    {
        public int ID { set; get; }

        public DateTime? ReturnedTime { set; get; }

        public string ReturnedParcel { set; get; }

        public decimal? ReturnedAmount { set; get; }

        public DateTime? ReturnedAmountTime { set; get; }

        public bool PaymentMethod { set; get; }

        public string TKN { set; get; }

        public string TKC { set; get; }

        public bool StatusPayment { set; get; }

        public int? DocumentNo { set; get; }

        public int? PurchaseOrderReturnID { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string DocumentTypeID { set; get; }

        public string DocumentID { set; get; }

        public string Description { set; get; }

        public virtual PurchaseOrderDetailViewModel PurchaseOrderDetail { set; get; }

        public virtual DocumentTypeViewModel DocumentType { set; get; }
    }
}