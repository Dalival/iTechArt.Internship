using System;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Repositories
{
    // I don't understand why do I need IDisposable. And why don't I use a destructor.
    public class UnitOfWork : IDisposable 
    {
        //redundant – I'll use DI
        //private SurveysDbContext _dbContext = new SurveysDbContext();
        private SurveysDbContext _dbContext;
        private SurveyRepository _surveyRepository;
        private bool _disposed = false;

        public SurveyRepository Surveys { get; }
        //redundant – I'll use DI
        // { get { return _surveyRepository ??= new SurveyRepository(_dbContext); } }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this._disposed = true;
            }
        }
    }
}