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

        public async Task<List<AuthError>> CreateUserAsync(User user, string password)
        {
            var errors = new List<AuthError>();

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                errors = ConvertErrors(result.Errors);
            }

            return errors;
        }


        private List<AuthError> ConvertErrors(IEnumerable<IdentityError> identityErrors)
        {
            var authErrors = new List<AuthError>();

            foreach (var identityError in identityErrors)
            {
                switch (identityError.Code)
                {
                    case nameof(IdentityErrorDescriber.InvalidUserName):
                        authErrors.Add(AuthError.InvalidUserName);
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateUserName):
                        authErrors.Add(AuthError.DuplicateUserName);
                        break;
                    case nameof(IdentityErrorDescriber.InvalidEmail):
                        authErrors.Add(AuthError.InvalidEmail);
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateEmail):
                        authErrors.Add(AuthError.DuplicateEmail);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordTooShort):
                        authErrors.Add(AuthError.PasswordTooShort);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresDigit):
                        authErrors.Add(AuthError.PasswordRequiresDigit);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresLower):
                        authErrors.Add(AuthError.PasswordRequiresLower);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresUpper):
                        authErrors.Add(AuthError.PasswordRequiresUpper);
                        break;
                    case nameof(IdentityErrorDescriber):
                        authErrors.Add(AuthError.PasswordRequiresUniqueChars);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric):
                        authErrors.Add(AuthError.PasswordRequiresNonAlphanumeric);
                        break;
                    default:
                        authErrors.Add(AuthError.DefaultError);
                        break;
                }
            }

            return authErrors;
        }
    }
}
