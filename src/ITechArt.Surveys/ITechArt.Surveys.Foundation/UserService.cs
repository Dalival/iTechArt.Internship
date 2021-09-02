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


        public async Task<RegistrationResult> CreateUserAsync(User user, string password, string passwordConfirmation)
        {
            if (password != passwordConfirmation)
            {
                return RegistrationResult.Failed(RegistrationError.PasswordConfirmationIncorrect);
            }

            var identityResult = await _userManager.CreateAsync(user, password);
            if (!identityResult.Succeeded)
            {
                var errors = ConvertErrors(identityResult.Errors).ToArray();
                return RegistrationResult.Failed(errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return RegistrationResult.Success;
        }


        private List<RegistrationError> ConvertErrors(IEnumerable<IdentityError> identityErrors)
        {
            var registrationErrors = new List<RegistrationError>();

            foreach (var identityError in identityErrors)
            {
                switch (identityError.Code)
                {
                    case nameof(IdentityErrorDescriber.InvalidUserName):
                        registrationErrors.Add(RegistrationError.InvalidUserName);
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateUserName):
                        registrationErrors.Add(RegistrationError.DuplicateUserName);
                        break;
                    case nameof(IdentityErrorDescriber.InvalidEmail):
                        registrationErrors.Add(RegistrationError.InvalidEmail);
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateEmail):
                        registrationErrors.Add(RegistrationError.DuplicateEmail);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordTooShort):
                        registrationErrors.Add(RegistrationError.PasswordTooShort);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresDigit):
                        registrationErrors.Add(RegistrationError.PasswordRequiresDigit);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresLower):
                        registrationErrors.Add(RegistrationError.PasswordRequiresLower);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresUpper):
                        registrationErrors.Add(RegistrationError.PasswordRequiresUpper);
                        break;
                    case nameof(IdentityErrorDescriber):
                        registrationErrors.Add(RegistrationError.PasswordRequiresUniqueChars);
                        break;
                    case nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric):
                        registrationErrors.Add(RegistrationError.PasswordRequiresNonAlphanumeric);
                        break;
                    default:
                        registrationErrors.Add(RegistrationError.UnknownError);
                        break;
                }
            }

            return registrationErrors;
        }
    }
}
