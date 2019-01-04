using System;

namespace tojitoji.WebApp.Models
{
    public class TrialBalanceViewModel
    {
        public int ID { set; get; }

        public string DocumentTypeID { set; get; }

        public DateTime Date { set; get; }

        public string DocumentID { set; get; }

        public string Description { set; get; }

        public int HumanID { set; get; }

        public string AccountID { set; get; }

        public string CorrespondingAccountID { set; get; }

        public decimal? DebitIncurred { set; get; }

        public decimal? CreditIncurred { set; get; }
    }
}