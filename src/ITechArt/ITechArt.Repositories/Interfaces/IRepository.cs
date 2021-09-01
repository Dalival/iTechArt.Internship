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

        Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        void Add(T entity);

        void AddRange(IReadOnlyCollection<T> entities);

        void Update(T entity);

        void UpdateRange(IReadOnlyCollection<T> entities);

        void Delete(T entity);

        void DeleteRange(IReadOnlyCollection<T> entities);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
