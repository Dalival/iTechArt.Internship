﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Result;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<RegistrationResult> CreateUserAsync(User user, string password, string passwordConfirmation);
    }
}
