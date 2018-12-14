using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("SKUsLazada")]
    public class SKULazada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Lazada_SKU { set; get; }

        [MaxLength(256)]
        public string SKUName { set; get; }

        public int? SKUID { set; get; }

        [Column(TypeName = "varchar")]
        public string Link { set; get; }

        public string Status { set; get; }

        [ForeignKey("SKUID")]
        public virtual SKU SKU { set; get; }
    }
}