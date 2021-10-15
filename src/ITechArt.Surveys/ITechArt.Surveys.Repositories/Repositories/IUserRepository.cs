﻿using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Repositories.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> CountUsersWithUsernameAsync(string userNameSearchString);
    }
}
