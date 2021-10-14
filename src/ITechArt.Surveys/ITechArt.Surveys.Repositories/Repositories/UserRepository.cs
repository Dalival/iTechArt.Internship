using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Common;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext) { }


        // public async Task<IReadOnlyCollection<User>> GetUsersPageAsync(
        //     int fromPosition,
        //     int amount,
        //     Expression<Func<User, object>> orderBy,
        //     bool descending = false,
        //     string searchString = null)
        // {
        //     var filteredUsers = searchString == null
        //         ? _dbSet
        //         : _dbSet.Where(u => u.UserName.Contains(searchString.Trim()));
        //
        //     var orderedUsers = (descending
        //             ? filteredUsers.OrderByDescending(orderBy)
        //             : filteredUsers.OrderBy(orderBy))
        //         .ThenBy(u => u.RegistrationDate);
        //
        //     var usersWithRoles = await orderedUsers
        //         .Skip(fromPosition)
        //         .Take(amount)
        //         .Include(u => u.UserRoles)
        //         .ThenInclude(ur => ur.Role)
        //         .ToListAsync();
        //
        //     return usersWithRoles;
        // }

        public override async Task<IReadOnlyCollection<User>> GetPaginatedAsync(
            int skipCount,
            int takeCount,
            params EntityOrderStrategy<User>[] orderStrategies)
        {
            var query = GetPaginatedCore(skipCount, takeCount, orderStrategies);
            var usersWithRoles = await GetWithRoleIncludes(query).ToListAsync();

            return usersWithRoles;
        }

        public override async Task<IReadOnlyCollection<User>> GetWherePaginatedAsync(
            int skipCount,
            int takeCount,
            Expression<Func<User, bool>> predicate,
            params EntityOrderStrategy<User>[] orderStrategies)
        {
            var query = GetPaginatedCore(skipCount, takeCount, orderStrategies);
            var filteredQuery = query.Where(predicate);
            var usersWithRoles = await GetWithRoleIncludes(filteredQuery).ToListAsync();

            return usersWithRoles;
        }

        public async Task<int> CountUsersWithUsernameAsync(string searchString)
        {
            var recordsAmount = await CountAsync(u => u.UserName == searchString);

            return recordsAmount;
        }


        private IQueryable<User> GetWithRoleIncludes(IQueryable<User> query)
        {
            var queryWithRoles = query
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role);

            return queryWithRoles;
        }
    }
}
