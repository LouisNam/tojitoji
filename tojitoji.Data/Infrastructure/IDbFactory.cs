using System;

namespace tojitoji.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        tojitojiDbContext Init();
    }
}