using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITechArt.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<T> GetByIdAsync(params object[] id);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyCollection<T>> GetWithIncludesAsync<TProperty>(params Expression<Func<T, TProperty>>[] navigationProperties);

        void Add(T entity);

        void AddRange(IReadOnlyCollection<T> entities);

        void Update(T entity);

        void UpdateRange(IReadOnlyCollection<T> entities);

        void Delete(T entity);

        void DeleteRange(IReadOnlyCollection<T> entities);
    }
}
