using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysUnitOfWork : UnitOfWork<SurveysDbContext>, ISurveysUnitOfWork
    {
        public IUserRepository UserRepository => (IUserRepository) GetRepository<User>();


        public SurveysUnitOfWork(SurveysDbContext context)
            : base(context)
        {
            RegisterRepository<User, UserRepository>();
        }
    }
}
