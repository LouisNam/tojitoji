using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tojitoji.Model.Models;

namespace tojitoji.WebApp.Models
{
    public class HumanTypeViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(50, ErrorMessage = "Maxlength is 50 character!")]
        public string Type { set; get; }

        public int? ParentID { set; get; }

        public virtual IEnumerable<Human> Humans { set; get; }
    }
}