using System;

namespace tojitoji.WebApp.Models
{
    public class InventoryTransactionViewModel
    {
        public int ID { set; get; }

        public DateTime ModifiedDate { set; get; }

        public DateTime CreatedDate { set; get; }

        public bool Type { set; get; }

        public int? ItemID { set; get; }

        public int? ParcelID { set; get; }

        public int? WarehouseID { set; get; }

        public bool Status { set; get; }

        public string Note { set; get; }
    }
}