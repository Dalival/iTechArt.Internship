using System;

namespace ITechArt.Surveys.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly SurveysDbContext _dbContext;
        
        private bool _disposed = false;

        
        public CounterRepository Counters { get; }
 

        public UnitOfWork(SurveysDbContext dbContext,
            CounterRepository counters)
        {
            _dbContext = dbContext;
            Counters = counters;
        }
        
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}
