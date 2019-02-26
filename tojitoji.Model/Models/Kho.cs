using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Khos")]
    public class Kho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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