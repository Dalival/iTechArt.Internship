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
        Task<IReadOnlyCollection<User>> GetPaginatedAsync(
            int fromPosition,
            int amount,
            Expression<Func<User, object>> orderBy,
            bool descending,
            string searchString);

        Task<int> CountAsync(string searchString = null);
    }
}
