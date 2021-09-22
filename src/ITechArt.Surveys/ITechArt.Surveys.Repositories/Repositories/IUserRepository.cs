﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IReadOnlyCollection<User>> GetAllWithRolesAsync();

        Task<IReadOnlyCollection<User>> GetPaginatedWithRolesAsync(int fromPosition, int amount);
    }
}