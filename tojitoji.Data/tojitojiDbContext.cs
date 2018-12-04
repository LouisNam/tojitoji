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

        //public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        //public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        //public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        //public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static tojitojiDbContext Create()
        {
            return new tojitojiDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder builder)
        //{
        //    builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
        //    builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
        //    builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        //    builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        //}
    }
}