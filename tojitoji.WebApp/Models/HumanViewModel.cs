using System;
using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class HumanViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string LastName { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        public int TypeCode { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Company { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(20)]
        public string Gender { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(15)]
        public string Phone { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Email { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string JobTitle { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Address { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Province { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string City { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string District { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Ward { set; get; }

        [MaxLength(256)]
        public string OtherContact { set; get; }

        public int? TaxCode { set; get; } // Mã số thuế

        [MaxLength(256)]
        public string Picture { set; get; }

        public string Note { set; get; }

        public DateTime? DateOfBirth { set; get; }

        public DateTime? DateOfEntry { set; get; }

        public virtual HumanTypeViewModel HumanType { set; get; }
    }
}