using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, IDbModel
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public async Task<T> Get(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }
        
        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }
        
        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }
        
        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
