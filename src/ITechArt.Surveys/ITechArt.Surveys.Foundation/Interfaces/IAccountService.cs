using System.Threading.Tasks;
using ITechArt.Common.Result;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult<LoginError>> LoginAsync(string emailOrUserName, string password);

        Task LogoutAsync();
    }
}
