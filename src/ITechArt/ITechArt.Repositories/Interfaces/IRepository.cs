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

        void AddRange(IReadOnlyCollection<T> entities);

        void Update(T entity);

        void UpdateRange(IReadOnlyCollection<T> entities);

        void Delete(T entity);

        void DeleteRange(IReadOnlyCollection<T> entities);
    }
}
