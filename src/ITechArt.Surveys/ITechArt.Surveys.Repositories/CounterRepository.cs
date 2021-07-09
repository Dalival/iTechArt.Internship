using System.Collections.Generic;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class CounterRepository : BaseRepository<SurveysDbContext, Counter>
    {
        public CounterRepository(SurveysDbContext dbContext)
            : base(dbContext) { }
    }
}