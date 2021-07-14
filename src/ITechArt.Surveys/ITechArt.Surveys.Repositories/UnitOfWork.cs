using System;
using System.Collections.Generic;
using ITechArt.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class UnitOfWork : IUnitOfWork<SurveysDbContext>
    {
        private Dictionary<(Type type, string name), object> _repositories;

        
        public SurveysDbContext Context { get; }

        
        public UnitOfWork(SurveysDbContext context)
        {
            Context = context;
        }
        
        
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return (IRepository<TEntity>) GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context));
        }
        
        public void Dispose()
        {
            Context?.Dispose();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
        
        
        internal object GetOrAddRepository(Type type, object repository)
        {
            _repositories ??= new Dictionary<(Type type, string name), object>();

            if (_repositories.TryGetValue((type, repository.GetType().FullName),
                out var newRepository))
            {
                return newRepository;
            }
            _repositories.Add((type, repository.GetType().FullName), repository);
            return repository;
        }
    }
}
