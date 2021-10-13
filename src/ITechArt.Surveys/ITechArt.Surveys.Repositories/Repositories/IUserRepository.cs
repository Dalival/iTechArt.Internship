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
        Task<IReadOnlyCollection<User>> GetUsersPageAsync(
            int fromPosition,
            int amount,
            Expression<Func<User, object>> orderBy,
            bool descending = false,
            string searchString = null);

        Task<int> CountUsersWithUsernameAsync(string searchString);
    }
}
