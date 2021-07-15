using System;
using System.Threading.Tasks;

namespace ITechArt.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
         IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IDbModel;

        Task Commit();
    }
}
