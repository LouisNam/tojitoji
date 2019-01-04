using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("TrialBalances")]
    public class TrialBalance // Phát sinh cân đối
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; } // Loại chứng từ

        public DateTime Date { set; get; } // Ngày chứng từ

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string DocumentID { set; get; } // Số chứng từ

        public string Description { set; get; }

        public int HumanID { set; get; } // Khách hàng

        public string AccountID { set; get; } // Mã tài khoản kế toán

        public string CorrespondingAccountID { set; get; } // Tài khoản đối ứng

        public decimal? DebitIncurred { set; get; } // Tài khoản phát sinh nợ

        public decimal? CreditIncurred { set; get; } // Tài khoản phát sinh có
    }
}