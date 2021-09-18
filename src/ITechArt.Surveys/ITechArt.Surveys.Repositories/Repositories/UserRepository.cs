using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyCollection<User>> GetAllIncludeRolesAsync()
        {
            var usersWithRoles = await _dbSet
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return usersWithRoles;
        }

        public async Task<IReadOnlyCollection<User>> GetRangeIncludeRolesAsync(int amount, int fromPosition = 0)
        {
            var usersWithRoles = await _dbSet
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Skip(fromPosition)
                .Take(amount)
                .ToListAsync();

            return usersWithRoles;
        }
    }
}
