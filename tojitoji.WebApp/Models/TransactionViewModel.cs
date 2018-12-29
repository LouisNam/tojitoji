using System;

namespace tojitoji.WebApp.Models
{
    public class TransactionViewModel
    {
        public int ID { set; get; }

        public DateTime Date { set; get; }

        public string Description { set; get; }

        public decimal Amount { set; get; }

        public int DocumentID { set; get; }

        public int HumanID { set; get; }

        public string TKN { set; get; }

        public string TKC { set; get; }

        public virtual DocumentViewModel Document { set; get; }

        public virtual HumanViewModel Human { set; get; }
    }
}