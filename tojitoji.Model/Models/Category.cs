using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int Code { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string Categories { set; get; }

        public int Categories_Type { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string MacroCategories { set; get; }

        [MaxLength(256)]
        public string CommercialCate { set; get; }

        [MaxLength(256)]
        public string Name_1 { set; get; }

        [MaxLength(256)]
        public string Name_2 { set; get; }

        [MaxLength(256)]
        public string Name_3 { set; get; }

        [MaxLength(256)]
        public string Name_4 { set; get; }

        [MaxLength(256)]
        public string Name_5 { set; get; }

        [MaxLength(256)]
        public string Name_6 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string NameEn_1 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string NameEn_2 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string NameEn_3 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string NameEn_4 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string NameEn_5 { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string NameEn_6 { set; get; }

        public virtual IEnumerable<Product> Products { set; get; }
    }
}