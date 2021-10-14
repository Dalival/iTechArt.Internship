using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Common.Result;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Enum;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password);

        Task<IReadOnlyCollection<User>> GetUsersPageAsync(
            int skip,
            int take,
            UserSortOrder? sortOrder,
            string searchString = null);

        Task<int> CountUsersAsync(string searchString = null);

        Task<bool>  DeleteUserAsync(string id);

        Task<OperationResult<AddingRoleError>> AddToRoleAsync(string userId, string roleName);

        Task<OperationResult<RemovingRoleError>> RemoveFromRoleAsync(string userId, string roleName);
    }
}
