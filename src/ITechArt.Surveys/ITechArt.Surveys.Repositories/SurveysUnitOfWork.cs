using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysUnitOfWork : UnitOfWork<SurveysDbContext>, ISurveysUnitOfWork
    {
        public UserRepository UserRepository => (UserRepository) GetRepository<User>();


        public SurveysUnitOfWork(SurveysDbContext context)
            : base(context)
        {
            RegisterRepository<User, UserRepository>();
        }
    }
}
