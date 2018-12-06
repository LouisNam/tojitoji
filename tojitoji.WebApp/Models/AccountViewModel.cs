using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class AccountViewModel
    {
        [Required]
        public string ID { set; get; }

        public bool? AccountType { set; get; }

        [Required]
        [MaxLength(256)]
        public string Account_Name { set; get; }

        [Required]
        public int? Account_1 { set; get; }

        [MaxLength(256)]
        public string Account_1_Name { set; get; }

        public int? Account_2 { set; get; }

        [MaxLength(256)]
        public string Account_2_Name { set; get; }

        public int? Account_3 { set; get; }

        [MaxLength(256)]
        public string Account_3_Name { set; get; }

        public bool? Status { set; get; }

        public string TKNhanKC { set; get; }

        public string MaKC { set; get; }
    }
}