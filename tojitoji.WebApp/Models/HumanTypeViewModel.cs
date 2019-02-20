using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tojitoji.WebApp.Models
{
    public class HumanTypeViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Type_1 { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Type_2 { set; get; }

        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Type_3 { set; get; }

        public virtual IEnumerable<HumanViewModel> Humans { set; get; }
    }
}