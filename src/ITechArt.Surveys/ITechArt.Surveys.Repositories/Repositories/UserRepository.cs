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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext) { }


        public async Task<IReadOnlyCollection<User>> GetAllWithRolesAsync()
        {
            var usersWithRoles = await _dbSet
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return usersWithRoles;
        }

        public async Task<IReadOnlyCollection<User>> GetPaginatedWithRolesAsync(int fromPosition, int amount,
            Expression<Func<User, object>> orderBy, bool descending = false)
        {
            var orderedUsers = descending
                ? _dbSet.OrderByDescending(orderBy)
                : _dbSet.OrderBy(orderBy);

            var usersWithRoles = await orderedUsers
                .Skip(fromPosition)
                .Take(amount)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return usersWithRoles;
        }
    }
}
