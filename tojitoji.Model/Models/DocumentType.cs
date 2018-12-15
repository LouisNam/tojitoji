using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("DocumentTypes")]
    public class DocumentType
    {
        [Key]
        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string ID { set; get; }

        [MaxLength(30)]
        public string Name { set; get; }

        public string Note { set; get; }
    }
}