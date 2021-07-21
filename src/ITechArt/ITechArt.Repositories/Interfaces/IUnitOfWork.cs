using System;
using System.Threading.Tasks;

namespace ITechArt.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        public TRepository GetCustomRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : Repository<TEntity>;

        Task SaveAsync();
    }
}
