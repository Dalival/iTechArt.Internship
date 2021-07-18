using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITechArt.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<T> GetAsync(params object[] keyValues);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        void Add(T entity);

        void Add(params T[] entities);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Update(T entity);

        void Update(params T[] entities);
    }
}
