using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext) { }


        public override async Task<IReadOnlyCollection<User>> GetPaginatedAsync(
            int skip,
            int take,
            params EntityOrderStrategy<User>[] orderStrategies)
        {
            var usersQuery = GetPaginatedQuery(_dbSet, skip, take, orderStrategies);
            var usersWithRoles = await GetUsersQueryWithRoles(usersQuery).ToListAsync();

            return usersWithRoles;
        }

        public override async Task<IReadOnlyCollection<User>> GetWherePaginatedAsync(
            int skip,
            int take,
            Expression<Func<User, bool>> predicate,
            params EntityOrderStrategy<User>[] orderStrategies)
        {
            var usersQuery = GetWherePaginatedQuery(_dbSet, skip, take, predicate, orderStrategies);
            var usersWithRoles = await GetUsersQueryWithRoles(usersQuery).ToListAsync();

            return usersWithRoles;
        }


        private IQueryable<User> GetUsersQueryWithRoles(IQueryable<User> usersQuery)
        {
            var usersQueryWithRoles = usersQuery
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role);

            return usersQueryWithRoles;
        }
    }
}
