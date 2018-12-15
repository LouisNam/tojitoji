using System;

namespace tojitoji.WebApp.Models
{
    public class PurchaseOrderViewModel
    {
        public int ID { set; get; }

        public DateTime CreatedDate { set; get; }

        public string DocumentTypeID { set; get; }

        public string DocumentID { set; get; }

        public string Description { set; get; }

        public int? SupplierID { set; get; }

        public int? SubmittedByID { set; get; }

        public DateTime? SubmittedDate { set; get; }

        public int? ApprovedByID { set; get; }

        public DateTime? ApprovedDate { set; get; }

        public string Note { set; get; }

        public string PurchasePlace { set; get; }

        public virtual HumanViewModel Supplier { set; get; }

        public virtual HumanViewModel Submit { set; get; }

        public virtual HumanViewModel Approve { set; get; }

        public virtual DocumentTypeViewModel DocumentType { set; get; }
    }
}