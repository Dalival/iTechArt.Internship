using System;
using System.Collections.Generic;
using System.Linq;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public abstract class BaseRepository<TModel> where TModel : BaseModel
    {
        protected SurveysDbContext _DbContext;
        protected DbSet<TModel> _dbSet;

        public BaseRepository(SurveysDbContext dbContext)
        {
            _DbContext = dbContext;
            _dbSet = _DbContext.Set<TModel>();
        }

        public List<TModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TModel Get(long id)
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
                    _DbContext.Entry(old).State = EntityState.Detached;
                }
                
                //doesn't work for some reason
                //_DbContext.Entry(model).State = EntityState.Modified;
                _dbSet.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }

            _DbContext.SaveChanges();
        }

        public void Remove(long id)
        {
            var model = Get(id);
            Remove(model);
        }

        public void Remove(TModel model)
        {
            _DbContext.Remove(model);
            _DbContext.SaveChanges();
        }
    }
}