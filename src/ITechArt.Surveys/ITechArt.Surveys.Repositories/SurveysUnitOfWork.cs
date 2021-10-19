using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysUnitOfWork : UnitOfWork<SurveysDbContext>
    {
        public SurveysUnitOfWork(SurveysDbContext context)
            : base(context)
        {
            RegisterRepository<User, UserRepository>();
        }
    }
}
