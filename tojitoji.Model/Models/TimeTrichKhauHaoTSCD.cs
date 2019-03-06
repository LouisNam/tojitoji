using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("TimeTrichKhauHaoTSCDs")]
    public class TimeTrichKhauHaoTSCD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        [Required]
        public string DanhMucNhomTSCD { set; get; }

        [MaxLength(256)]
        [Required]
        public string NhomTSCD { set; get; }

        public int TimeMin { set; get; }

        public int TimeMax { set; get; }
    }
}