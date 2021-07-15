using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITechArt.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class, IDbModel
    {
        Task<T> Get(int id);
        Task<IReadOnlyCollection<T>> GetAll();
    
        void Add(T entity);
        void Add(params T[] entities);
        void Add(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}
