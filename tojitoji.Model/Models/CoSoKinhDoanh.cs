using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("CoSoKinhDoanhs")]
    public class CoSoKinhDoanh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        public string Place_1 { set; get; }

        [MaxLength(256)]
        public string Place_2 { set; get; }

        [MaxLength(256)]
        public string Place_3 { set; get; }

        [MaxLength(256)]
        public string Place_4 { set; get; }

        public bool Status { set; get; }

        [MaxLength(256)]
        public string Note { set; get; }
    }
}