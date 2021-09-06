using System.Linq;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Foundation.Result;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation
{
    public class UserService : IUserService
    {
        private readonly ICustomLogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public UserService(ICustomLogger logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password, string passwordConfirmation)
        {
            var identityResult = await _userManager.CreateAsync(user, password);
            var operationResult = ConvertResult(identityResult);

            if (operationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return operationResult;
        }

        public async Task<OperationResult<LoginError>> LoginAsync(string emailOrUserName, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailOrUserName)
                       ?? await _userManager.FindByNameAsync(emailOrUserName);
            if (user == null)
            {
                return OperationResult<LoginError>.Failed(LoginError.EmailAndUserNameNotFound);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, true, true);
            var operationResult = ConvertResult(signInResult);

            if (operationResult.Succeeded)
            {
                _logger.LogInformation("User logged in.");
            }

            return operationResult;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }


        private OperationResult<RegistrationError> ConvertResult(IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return OperationResult<RegistrationError>.Success;
            }

            var registrationErrors = identityResult.Errors.Select(identityError => identityError.Code switch
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

            var operationResult = OperationResult<RegistrationError>.Failed(registrationErrors);

            return operationResult;
        }

        private OperationResult<LoginError> ConvertResult(SignInResult signInResult)
        {
            if (signInResult.Succeeded)
            {
                return OperationResult<LoginError>.Success;
            }

            var error = signInResult.IsLockedOut ? LoginError.AccountLockedOut :
                signInResult.IsNotAllowed ? LoginError.NotAllowedToLogin :
                LoginError.WrongPassword;

            return OperationResult<LoginError>.Failed(error);
        }
    }
}
