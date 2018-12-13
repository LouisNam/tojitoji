using System;
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

        public int CategoryType { set; get; }

        [Column(TypeName = "varchar")]
        public string MacroCategory { set; get; }

        public string CommercialCate { set; get; }

        public string Name { set; get; }

        [Column(TypeName = "varchar")]
        public string NameEn { set; get; }

        public int? ParentID { set; get; }

        public virtual IEnumerable<Product> Products { set; get; }
    }
}