using System;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Foundation.Result;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation
{
    public class AccountService : IAccountService
    {
        private readonly ICustomLogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountService(ICustomLogger logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<OperationResult<LoginError>> LoginAsync(string emailOrUserName, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailOrUserName);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(emailOrUserName);
            }

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
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, "User can't sign out!");
            }
        }


        private OperationResult<LoginError> ConvertResult(SignInResult signInResult)
        {
            if (signInResult.Succeeded)
            {
                return OperationResult<LoginError>.Success;
            }

            var error = signInResult.IsLockedOut
                ? LoginError.AccountLockedOut
                : LoginError.WrongPassword;

            return OperationResult<LoginError>.Failed(error);
        }
    }
}
