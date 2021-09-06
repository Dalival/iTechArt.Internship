using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Foundation.Result;
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


        public async Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password, string passwordConfirmation)
        {
            var identityResult = await _userManager.CreateAsync(user, password);
            if (!identityResult.Succeeded)
            {
                var errors = ConvertErrors(identityResult.Errors);

                return OperationResult<RegistrationError>.Failed(errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return OperationResult<RegistrationError>.Success;
        }


        private List<RegistrationError> ConvertErrors(IEnumerable<IdentityError> identityErrors)
        {
            var registrationErrors = identityErrors.Select(identityError => identityError.Code switch
                {
                    nameof(IdentityErrorDescriber.InvalidUserName) => RegistrationError.InvalidUserName,
                    nameof(IdentityErrorDescriber.DuplicateUserName) => RegistrationError.DuplicateUserName,
                    nameof(IdentityErrorDescriber.InvalidEmail) => RegistrationError.InvalidEmail,
                    nameof(IdentityErrorDescriber.DuplicateEmail) => RegistrationError.DuplicateEmail,
                    nameof(IdentityErrorDescriber.PasswordTooShort) => RegistrationError.PasswordTooShort,
                    nameof(IdentityErrorDescriber.PasswordRequiresDigit) => RegistrationError.PasswordRequiresDigit,
                    nameof(IdentityErrorDescriber.PasswordRequiresLower) => RegistrationError.PasswordRequiresLower,
                    nameof(IdentityErrorDescriber.PasswordRequiresUpper) => RegistrationError.PasswordRequiresUpper,
                    nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars) => RegistrationError.PasswordRequiresMoreUniqueChars,
                    nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => RegistrationError.PasswordRequiresNonAlphanumeric,
                    _ => RegistrationError.UnknownError
                })
                .ToList();

            return registrationErrors;
        }
    }
}
