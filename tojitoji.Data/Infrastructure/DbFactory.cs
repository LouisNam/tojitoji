namespace tojitoji.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private tojitojiDbContext dbContext;

        public tojitojiDbContext Init()
        {
            return dbContext ?? (dbContext = new tojitojiDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}