using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("ClosingEntries")]
    public class ClosingEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; } // Số chứng từ

        [Column(TypeName = "varchar")]
        [MaxLength(2)]
        public string DocumentTypeID { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string DocumentID { set; get; }

        public DateTime Date { set; get; }

        public string Description { set; get; }

        public virtual IEnumerable<ClosingEntryDetail> ClosingEntryDetails { set; get; }
    }
}