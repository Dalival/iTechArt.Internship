using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Result;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password);

        Task<IReadOnlyCollection<User>> GetAllUsersAsync();

        Task<IReadOnlyCollection<User>> GetPaginatedUsersAsync(int fromPosition, int amount);

        Task<int> CountUsersAsync();

        Task<OperationResult<UserManagementError>>  DeleteUserAsync(string id);
    }
}
