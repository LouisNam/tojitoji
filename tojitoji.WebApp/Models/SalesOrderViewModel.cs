using System;
using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class SalesOrderViewModel
    {
        public int ID { set; get; }

        public DateTime CreatedDate { set; get; }

        public string DocumentTypeID { set; get; }

        public string DocumentID { set; get; }

        public string Description { set; get; }

        public int CustomerID { set; get; }

        public int StaffID { set; get; }

        [MaxLength(256)]
        public string SalesPlace { set; get; }

        public int WarehouseID { set; get; }

        public string PartnerOrderID { set; get; }

        public virtual DocumentTypeViewModel DocumentType { set; get; }

        public virtual HumanViewModel Customer { set; get; }

        public virtual HumanViewModel Staff { set; get; }

        public virtual WarehouseViewModel Warehouse { set; get; }
    }
}