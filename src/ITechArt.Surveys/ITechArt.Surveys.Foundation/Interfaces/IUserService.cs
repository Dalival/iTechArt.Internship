﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Common.Result;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password);

        Task<IReadOnlyCollection<User>> GetPaginatedUsersAsync(int fromPosition, int amount,
            Expression<Func<User, object>> orderBy, bool descending = false);

        Task<int> CountUsersAsync();

        Task<bool>  DeleteUserAsync(string id);

        Task<OperationResult<AddingRoleErrors>> AddToRoleAsync(string userId, string roleName);

        Task<OperationResult<RemovingRoleError>> RemoveFromRoleAsync(string userId, string roleName);
    }
}
