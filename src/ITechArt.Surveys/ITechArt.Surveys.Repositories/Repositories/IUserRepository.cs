using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IReadOnlyCollection<User>> GetAllWithRolesAsync();

        Task<IReadOnlyCollection<User>> GetWhereWithRolesAsync(Expression<Func<User, bool>> predicate,
            Expression<Func<User, object>> orderBy, bool descending = false);

        Task<IReadOnlyCollection<User>> GetPaginatedWithRolesAsync(int fromPosition, int amount,
            Expression<Func<User, object>> orderBy, bool descending = false);
    }
}
