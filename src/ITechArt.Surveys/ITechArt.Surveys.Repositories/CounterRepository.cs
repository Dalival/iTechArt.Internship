using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Repositories
{
    public class CounterRepository : BaseRepository<SurveysDbContext, Counter>
    {
        public CounterRepository(SurveysDbContext dbContext)
            : base(dbContext) { }
    }
}
