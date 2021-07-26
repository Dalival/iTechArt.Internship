using System;
using System.Threading.Tasks;

namespace ITechArt.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        void RegisterRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IRepository<TEntity>;

        Task SaveAsync();
    }
}
