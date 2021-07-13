using System.Collections.Generic;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class SurveyRepository : BaseRepository<SurveysDbContext, Survey>
    {
        public SurveyRepository(SurveysDbContext dbContext)
            : base(dbContext) { }
    }
}
