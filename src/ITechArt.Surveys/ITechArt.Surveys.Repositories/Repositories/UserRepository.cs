using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }


        public async Task<IReadOnlyCollection<User>> GetAllWithRolesAsync()
        {
            var usersWithRoles = await _dbSet
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return usersWithRoles;
        }
    }
}
