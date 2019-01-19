using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Bibles")]
    public class Bible
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(20)]
        public string Shortcut { set; get; }

        [MaxLength(100)]
        public string Meaning { set; get; }
    }
}