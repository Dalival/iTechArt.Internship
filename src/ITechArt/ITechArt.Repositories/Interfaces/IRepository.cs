using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITechArt.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<T> GetByIdAsync(params object[] id);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        void Add(T entity);

        void Add(params T[] entities);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Update(T entity);

        void Update(params T[] entities);
    }
}
