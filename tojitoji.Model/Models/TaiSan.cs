using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("TaiSans")]
    public class TaiSan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        public string Name { set; get; }

        public int LoaiTaiSanID { set; get; }

        [ForeignKey("LoaiTaiSanID")]
        public virtual LoaiTaiSan LoaiTaiSan { set; get; }
    }
}