using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Common.Result;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Repositories;
using ITechArt.Surveys.Repositories.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation
{
    public class UserService : IUserService
    {
        private readonly ICustomLogger _logger;
        private readonly UserManager<User> _userManager;

        private readonly IUserRepository _userRepository;
        private readonly IRepository<Role> _roleRepository;


        public UserService(ICustomLogger logger, UserManager<User> userManager, ISurveysUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;

            _userRepository = unitOfWork.UserRepository;
            _roleRepository = unitOfWork.GetRepository<Role>();
        }


        public async Task<OperationResult<RegistrationError>> CreateUserAsync(User user, string password)
        {
            user.RegistrationDate = DateTime.Now;
            var identityResult = await _userManager.CreateAsync(user, password);
            var operationResult = ConvertResult(identityResult);

            if (operationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return operationResult;
        }

        public async Task<IReadOnlyCollection<User>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllWithRolesAsync();

            return users;
        }

        public async Task<IReadOnlyCollection<Role>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles;
        }

        public async Task<IReadOnlyCollection<User>> GetPaginatedUsersAsync(int fromPosition, int amount)
        {
            var users = await _userRepository.GetPaginatedWithRolesAsync(fromPosition, amount);

            return users;
        }

        public async Task<int> CountUsersAsync()
        {
            var amount = await _userRepository.CountAsync();

            return amount;
        }

        public async Task<OperationResult<UserManagementError>> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return OperationResult<UserManagementError>.Failed(UserManagementError.CannotDeleteUser);
            }

            var identityResult = await _userManager.DeleteAsync(user);
            var operationResult = identityResult.Succeeded
                ? OperationResult<UserManagementError>.Success
                : OperationResult<UserManagementError>.Failed(UserManagementError.CannotDeleteUser);

            return operationResult;
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
    }
}
