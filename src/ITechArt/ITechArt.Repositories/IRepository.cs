using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITechArt.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyCollection<T>> GetAllAsync();

        void Add(T entity);
        void Add(params T[] entities);
        void Add(IReadOnlyCollection<T> entities);

        void Delete(T entity);
        void Delete(params T[] entities);
        void Delete(IReadOnlyCollection<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IReadOnlyCollection<T> entities);
    }
}
