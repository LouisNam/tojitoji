using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("HumanTypes")]
    public class HumanType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Type_1 { set; get; }

        [MaxLength(50)]
        public string Type_2 { set; get; }

        [MaxLength(50)]
        public string Type_3 { set; get; }

        public virtual IEnumerable<Human> Humans { set; get; }
    }
}