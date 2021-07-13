using System.Collections.Generic;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class SectionRepository : BaseRepository<SurveysDbContext, Section>
    {
        public SectionRepository(SurveysDbContext dbContext)
            : base(dbContext) { }
    }
}
