//using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using tojitoji.Model.Models;

namespace tojitoji.Data
{
    public class tojitojiDbContext : DbContext
    {
        public tojitojiDbContext() : base("tojitojiConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Error> Errors { set; get; }
        public DbSet<Account> Accounts { set; get; }
        public DbSet<DocumentType> DocumentTypes { set; get; }
        public DbSet<CompanyInformation> CompanyInformations { set; get; }
        public DbSet<Human> Humans { set; get; }
        public DbSet<HumanType> HumanTypes { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Bundle> Bundles { set; get; }
        public DbSet<SKU> SKUs { set; get; }
        public DbSet<SKULazada> SKUsLazada { set; get; }
        public DbSet<InventoryTransaction> InventoryTransactions { set; get; }        
        public DbSet<PurchaseOrder> PurchaseOrders { set; get; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { set; get; }
        public DbSet<PurchaseOrderDetailReturn> PurchaseOrderDetailReturns { set; get; }
        public DbSet<SalesOrder> SalesOrders { set; get; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { set; get; }
        public DbSet<SalesOrderDetailReturn> SalesOrderDetailReturns { set; get; }
        public DbSet<Document> Documents { set; get; }
        public DbSet<Transaction> Transactions { set; get; }
        public DbSet<TrialBalance> TrialBalances { set; get; }
        public DbSet<Bible> Bibles { set; get; }
        public DbSet<CoSoKinhDoanh> CoSoKinhDoanhs { set; get; }
        public DbSet<LoaiTaiSan> LoaiTaiSans { set; get; }
        public DbSet<TaiSan> TaiSans { set; get; }
        public DbSet<LoaiKho> LoaiKhos { set; get; }
        public DbSet<Kho> Khos { set; get; }
        public DbSet<TimeTrichKhauHaoTSCD> TimeTrichKhauHaoTSCDs { set; get; }
        public DbSet<TangGiamTSCD> TangGiamTSCDs { set; get; }

        //public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        //public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        //public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        //public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static tojitojiDbContext Create()
        {
            return new tojitojiDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            //builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            //builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            //builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
            //builder.Entity<Human>()
            //        .HasRequired(m => m.HumanType)
            //        .WithMany(t => t.Humans)
            //        .HasForeignKey(m => m.TypeID)
            //        .WillCascadeOnDelete(false);
        }
    }
}