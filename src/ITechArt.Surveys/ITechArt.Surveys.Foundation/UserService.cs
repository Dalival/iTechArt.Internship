using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var operationResult = ConvertRegistrationResult(identityResult);

            if (operationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return operationResult;
        }

        public async Task<IReadOnlyCollection<User>> GetUsersPageAsync(
            int fromPosition,
            int amount,
            Expression<Func<User, object>> orderBy = null,
            bool descending = false,
            string searchString = null)
        {
            if (orderBy == null)
            {
                orderBy = user => user.RegistrationDate;
                descending = true;
            }

            var users = await _userRepository.GetUsersPageAsync(fromPosition, amount, orderBy, descending, searchString);

            return users;
        }

        public async Task<int> CountUsersAsync(string searchString = null)
        {
            var amount = searchString == null
                ? await _userRepository.CountAsync()
                : await _userRepository.CountUsersWithUsernameAsync(searchString);

            return amount;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Admin tried to delete non-existent user.");

                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<OperationResult<AddingRoleError>> AddToRoleAsync(string userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return OperationResult<AddingRoleError>.Failed(AddingRoleError.UserNotFound);
            }

            var isRoleExist = await _roleRepository.AnyAsync(r => r.NormalizedName == roleName.Normalize());
            if (!isRoleExist)
            {
                return OperationResult<AddingRoleError>.Failed(AddingRoleError.RoleNotFound);
            }

            var identityResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!identityResult.Succeeded)
            {
                return identityResult.Errors.Any(e => e.Code is nameof(IdentityErrorDescriber.UserAlreadyInRole))
                    ? OperationResult<AddingRoleError>.Failed(AddingRoleError.UserAlreadyInRole)
                    : OperationResult<AddingRoleError>.Failed(AddingRoleError.UnknownError);
            }

            return OperationResult<AddingRoleError>.Success;
        }

        public async Task<OperationResult<RemovingRoleError>> RemoveFromRoleAsync(string userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return OperationResult<RemovingRoleError>.Failed(RemovingRoleError.UserNotFound);
            }

            var identityResult = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!identityResult.Succeeded)
            {
                return identityResult.Errors.Any(e => e.Code is nameof(IdentityErrorDescriber.UserNotInRole))
                    ? OperationResult<RemovingRoleError>.Failed(RemovingRoleError.UserNotInRole)
                    : OperationResult<RemovingRoleError>.Failed(RemovingRoleError.UnknownError);
            }

            return OperationResult<RemovingRoleError>.Success;
        }


        private OperationResult<RegistrationError> ConvertRegistrationResult(IdentityResult identityResult)
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
