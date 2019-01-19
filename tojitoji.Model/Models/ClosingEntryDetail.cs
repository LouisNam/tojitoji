using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("ClosingEntryDetails")]
    public class ClosingEntryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int ClosingEntryID { set; get; }

        public string Description { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string DebitAccount { set; get; } // Tài khoản phát sinh nợ

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string CreditAccount { set; get; } // Tài khoản phát sinh có

        public decimal Amount { set; get; }

        [ForeignKey("ClosingEntryID")]
        public virtual ClosingEntry ClosingEntry { set; get; }
    }
}