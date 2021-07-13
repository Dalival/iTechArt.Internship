using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public abstract class BaseRepository<TContext, TModel>
        where TContext : DbContext
        where TModel : BaseModel
    {
        protected TContext _dbContext;
        protected DbSet<TModel> _dbSet;

        public BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TModel>();
        }

        public List<TModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TModel Get(int id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public void Save(TModel model)
        {
            if (model.Id > 0)
            {
                var old = _dbSet.Find(model.Id);

                if (old != null)
                {
                    _dbContext.Entry(old).State = EntityState.Detached;
                }
                
                //doesn't work for some reason
                //_dbContext.Entry(model).State = EntityState.Modified;
                _dbSet.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }

            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var model = Get(id);
            Remove(model);
        }

        public void Remove(TModel model)
        {
            _dbContext.Remove(model);
            _dbContext.SaveChanges();
        }
    }
}
