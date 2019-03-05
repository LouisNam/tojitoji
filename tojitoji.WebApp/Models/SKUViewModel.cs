namespace tojitoji.WebApp.Models
{
    public class SKUViewModel
    {
        public int ID { set; get; }

        public int? ProductID { set; get; }

        public int? BundleID { set; get; }

        public virtual ProductViewModel Product { set; get; }

        public virtual BundleViewModel Bundle { set; get; }
    }
}