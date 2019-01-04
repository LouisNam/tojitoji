using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [Column(TypeName = "varchar")]
        [MaxLength(10)]
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

        public decimal? SoDuNoDau { set; get; }

        public decimal? SoDuCoDau { set; get; }

        public DateTime? NgaySoDu { set; get; }

        public bool? Status { set; get; }

        public string TKNhanKC { set; get; }

        public string MaKC { set; get; }
    }
}