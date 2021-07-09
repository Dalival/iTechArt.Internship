using System.Collections.Generic;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Repositories
{
    public class SurveyRepository : BaseRepository<Survey>
    {
        public SurveyRepository(SurveysDbContext dbContext)
            : base(dbContext) { }
    }
}