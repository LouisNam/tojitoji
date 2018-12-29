using System;

namespace tojitoji.WebApp.Models
{
    public class DocumentViewModel
    {
        public int ID { set; get; }
        public string DocumentTypeID { set; get; }

        public DateTime Date { set; get; }

        public string Description { set; get; }

        public int HumanID { set; get; }

        public string Serial { set; get; }

        public string BillNo { set; get; }

        public DateTime? BillDate { set; get; }

        public virtual DocumentTypeViewModel DocumentType { set; get; }

        public virtual HumanViewModel Human { set; get; }
    }
}