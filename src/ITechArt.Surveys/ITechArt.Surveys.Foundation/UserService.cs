using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomLogger _logger;
        private readonly UserManager<User> _userManager;

        public UserService(ICustomLogger logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<AuthenticationError>> CreateUserAsync(User user, string password)
        {
            var errors = new List<AuthenticationError>();

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                errors = ConvertErrors(result.Errors);
            }

            return errors;
        }


        private List<AuthenticationError> ConvertErrors(IEnumerable<IdentityError> identityErrors)
        {
            var authErrors = new List<AuthenticationError>();

            foreach (var identityError in identityErrors)
            {
                switch (identityError.Code)
                {
                    case nameof(IdentityErrorDescriber.InvalidUserName):
                        authErrors.Add(AuthenticationError.InvalidUserName);
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateUserName):
                        authErrors.Add(AuthenticationError.DuplicateUserName);
                        break;
                    case nameof(IdentityErrorDescriber.InvalidEmail):
                        authErrors.Add(AuthenticationError.InvalidEmail);
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateEmail):
                        authErrors.Add(AuthenticationError.DuplicateEmail);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordTooShort):
                        authErrors.Add(AuthenticationError.PasswordTooShort);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresDigit):
                        authErrors.Add(AuthenticationError.PasswordRequiresDigit);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresLower):
                        authErrors.Add(AuthenticationError.PasswordRequiresLower);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresUpper):
                        authErrors.Add(AuthenticationError.PasswordRequiresUpper);
                        break;
                    case nameof(IdentityErrorDescriber):
                        authErrors.Add(AuthenticationError.PasswordRequiresUniqueChars);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric):
                        authErrors.Add(AuthenticationError.PasswordRequiresNonAlphanumeric);
                        break;
                    default:
                        authErrors.Add(AuthenticationError.UnknownError);
                        break;
                }
            }

            return authErrors;
        }
    }
}
