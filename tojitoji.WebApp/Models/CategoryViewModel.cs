using System.Collections.Generic;

namespace tojitoji.WebApp.Models
{
    public class CategoryViewModel
    {
        public int ID { set; get; }

        public int CategoryType { set; get; }

        public string MacroCategory { set; get; }

        public string CommercialCate { set; get; }

        public string Name { set; get; }

        public string NameEn { set; get; }

        public int? ParentID { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
    }
}