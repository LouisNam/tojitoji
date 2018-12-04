using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tojitoji.Model.Abstract;

namespace tojitoji.Model.Models
{
    [Table("Accounts")]
    public class Account : Auditable
    {
        [Key]
        public string ID { set; get; }

        public bool? AccountType { set; get; } // Loại Tài Khoản, Quy ước: TK phát sinh tăng ở bên nợ thì là "N", TK phát sinh tăng ở bên có ghi là "C"

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

        public bool? Status { set; get; } // Có định khoản

        public string TKNhanKC { set; get; } // Tài khoản đối ứng được kết chuyển cuối tháng

        public string MaKC { set; get; } // Mã Kế chuyển cho biết tài khoản sẽ được kết chuyện trong nhóm các bút toán nào
    }
}