using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tojitoji.Model.Models
{
    [Table("Humans")]
    public class Human
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string FirstName { set; get; }

        [Required]
        [MaxLength(50)]
        public string LastName { set; get; }

        [Required]
        public int TypeCode { set; get; }

        [MaxLength(50)]
        public string Company { set; get; }

        [Required]
        [MaxLength(20)]
        public string Gender { set; get; }

        [Required]
        [MaxLength(15)]
        public string Phone { set; get; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Email { set; get; }

        [MaxLength(50)]
        public string JobTitle { set; get; }

        [MaxLength(50)]
        public string Address { set; get; }

        [MaxLength(50)]
        public string Province { set; get; }

        [MaxLength(50)]
        public string City { set; get; }

        [MaxLength(50)]
        public string District { set; get; }

        [MaxLength(50)]
        public string Ward { set; get; }

        [MaxLength(256)]
        public string OtherContact { set; get; }

        public int? TaxCode { set; get; } // Mã số thuế

        [MaxLength(256)]
        public string Picture { set; get; }

        public string Note { set; get; }

        public DateTime? DateOfBirth { set; get; }

        public DateTime? DateOfEntry { set; get; }

        [ForeignKey("TypeCode")]
        public virtual HumanType HumanType { set; get; }
    }
}