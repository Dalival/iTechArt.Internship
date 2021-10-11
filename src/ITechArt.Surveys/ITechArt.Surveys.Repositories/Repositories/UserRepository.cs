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


        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            var usersWithRoles = await _dbSet
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return usersWithRoles;
        }

        public async Task<IReadOnlyCollection<User>> GetPaginatedAsync(
            int fromPosition,
            int amount,
            Expression<Func<User, object>> orderBy,
            bool descending = false,
            string searchString = null)
        {
            var normalizedSearchString = searchString?.ToUpper().Trim();
            var targetUsers = normalizedSearchString == null
                ? _dbSet
                : _dbSet.Where(user => user.NormalizedUserName.Contains(normalizedSearchString));

            var orderedUsers = (descending
                    ? targetUsers.OrderByDescending(orderBy)
                    : targetUsers.OrderBy(orderBy))
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
                : await _dbSet.CountAsync(u => u.UserName == searchString);

            return recordsAmount;
        }
    }
}
