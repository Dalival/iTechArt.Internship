using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Result;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult<LoginError>> LoginAsync(string emailOrUserName, string password);

        Task LogoutAsync();
    }
}
