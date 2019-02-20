using System.Collections.Generic;

namespace tojitoji.WebApp.Models
{
    public class CategoryViewModel
    {
        public int ID { set; get; }

        public int Code { set; get; }

        public string Categories { set; get; }

        public int Categories_Type { set; get; }

        public string MacroCategories { set; get; }

        public string CommercialCate { set; get; }

        public string Name_1 { set; get; }

        public string Name_2 { set; get; }

        public string Name_3 { set; get; }

        public string Name_4 { set; get; }

        public string Name_5 { set; get; }

        public string Name_6 { set; get; }

        public string NameEn_1 { set; get; }

        public string NameEn_2 { set; get; }

        public string NameEn_3 { set; get; }

        public string NameEn_4 { set; get; }

        public string NameEn_5 { set; get; }

        public string NameEn_6 { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
    }
}