using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<List<AuthenticationError>> CreateUserAsync(User user, string password);
    }
}
