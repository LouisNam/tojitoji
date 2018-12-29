using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public DateTime Date { set; get; } // Ngày tháng ghi sổ

        public string Description { set; get; }

        public decimal Amount { set; get; }

        public int DocumentID { set; get; } // Số chứng từ

        public int HumanID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string TKN { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string TKC { set; get; }

        [ForeignKey("DocumentID")]
        public virtual Document Document { set; get; }

        [ForeignKey("HumanID")]
        public virtual Human Human { set; get; }
    }
}