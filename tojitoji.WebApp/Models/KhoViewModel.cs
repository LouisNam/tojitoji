using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class KhoViewModel
    {
        public int ID { set; get; }

        [MaxLength(256)]
        public string Kho_1 { set; get; }

        [MaxLength(256)]
        public string Kho_2 { set; get; }

        [MaxLength(256)]
        public string Kho_3 { set; get; }

        [MaxLength(256)]
        public string Kho_4 { set; get; }

        public bool Status { set; get; }

        [MaxLength(256)]
        public string Note { set; get; }
    }
}