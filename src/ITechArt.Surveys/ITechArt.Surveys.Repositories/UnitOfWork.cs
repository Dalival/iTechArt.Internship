using System;

namespace ITechArt.Surveys.Repositories
{
    // I don't know do I need IDisposable
    public class UnitOfWork : IDisposable
    {
        private SurveysDbContext _dbContext;
        public SurveyRepository Surveys { get; set; }
        public SectionRepository Sections { get; set; }
 
        private bool _disposed = false;

        public UnitOfWork(SurveysDbContext dbContext,
            SurveyRepository surveys,
            SectionRepository sections)
        {
            _dbContext = dbContext;
            Surveys = surveys;
            Sections = sections;
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