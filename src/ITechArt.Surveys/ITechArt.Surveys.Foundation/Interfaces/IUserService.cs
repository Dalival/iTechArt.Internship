using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Common.Result;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password);

        Task<IReadOnlyCollection<User>> GetAllUsersAsync();

        Task<IReadOnlyCollection<Role>> GetAllRolesAsync();

        Task<IReadOnlyCollection<User>> GetPaginatedUsersAsync(int fromPosition, int amount);

        Task<int> CountUsersAsync();

        Task<OperationResult<UserManagementError>>  DeleteUserAsync(string id);
    }
}
