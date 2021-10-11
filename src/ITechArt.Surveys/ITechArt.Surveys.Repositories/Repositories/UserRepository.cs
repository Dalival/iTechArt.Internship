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


        public async Task<IReadOnlyCollection<User>> GetPaginatedAsync(
            int fromPosition,
            int amount,
            Expression<Func<User, object>> orderBy,
            bool descending = false,
            string searchString = null)
        {
            var filteredUsers = searchString == null
                ? _dbSet
                : _dbSet.Where(u => u.UserName.Contains(searchString.Trim()));

            var orderedUsers = (descending
                    ? filteredUsers.OrderByDescending(orderBy)
                    : filteredUsers.OrderBy(orderBy))
                .ThenBy(u => u.RegistrationDate);

            var usersWithRoles = await orderedUsers
                .Skip(fromPosition)
                .Take(amount)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return usersWithRoles;
        }

        public async Task<int> CountAsync(string searchString = null)
        {

            var recordsAmount = searchString == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(u => u.UserName.Contains(searchString.Trim()));

            return recordsAmount;
        }
    }
}
