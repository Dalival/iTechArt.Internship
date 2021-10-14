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


        public override async Task<IReadOnlyCollection<User>> GetPaginatedAsync(
            int skip,
            int take,
            params EntityOrderStrategy<User>[] orderStrategies)
        {
            var query = GetPaginatedCore(_dbSet, skip, take, orderStrategies);
            var usersWithRoles = await GetWithRoleIncludes(query).ToListAsync();

            return usersWithRoles;
        }

        public override async Task<IReadOnlyCollection<User>> GetWherePaginatedAsync(
            int skip,
            int take,
            Expression<Func<User, bool>> predicate,
            params EntityOrderStrategy<User>[] orderStrategies)
        {
            var filteredQuery = _dbSet.Where(predicate);
            var targetQuery = GetPaginatedCore(filteredQuery, skip, take, orderStrategies);
            var usersWithRoles = await GetWithRoleIncludes(targetQuery).ToListAsync();

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
