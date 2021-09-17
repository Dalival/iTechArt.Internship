using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class UnitOfWork : UnitOfWork<SurveysDbContext>
    {
        public UnitOfWork(SurveysDbContext context)
            : base(context)
        {
            RegisterRepository<User, UserRepository>();
        }
    }
}
