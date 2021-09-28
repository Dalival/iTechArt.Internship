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

        Task<IReadOnlyCollection<User>> GetPaginatedUsersAsync(int fromPosition, int amount);

        Task<int> CountUsersAsync();

        Task<bool>  DeleteUserAsync(string id);

        Task<OperationResult<UserManagementError>> AddToRoleAsync(string userId, string roleName);

        Task<OperationResult<UserManagementError>> RemoveFromRoleAsync(string userId, string roleName);
    }
}
