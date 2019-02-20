using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("LoaiTaiSans")]
    public class LoaiTaiSan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        public string Name { set; get; }
    }
}