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
        public string Type { set; get; }

        public int? ParentID { set; get; }

        public virtual IEnumerable<Human> Humans { set; get; }
    }
}