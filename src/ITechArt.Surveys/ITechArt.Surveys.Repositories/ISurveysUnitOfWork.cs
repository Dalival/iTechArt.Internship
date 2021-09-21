using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.Repositories.Repositories;

namespace ITechArt.Surveys.Repositories
{
    public interface ISurveysUnitOfWork : IUnitOfWork
    {
        UserRepository UserRepository { get; }
    }
}
