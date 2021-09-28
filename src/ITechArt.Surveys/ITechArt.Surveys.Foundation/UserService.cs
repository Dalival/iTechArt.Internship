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
            var operationResult = ConvertRegistrationResult(identityResult);

            if (operationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return operationResult;
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

        public async Task<OperationResult<GiveAdminRightsError>> GiveAdminRights(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.UserNotFound);
            }

            var isAdminRoleExist = await _roleRepository.AnyAsync(r => r.NormalizedName == "ADMIN");
            if (!isAdminRoleExist)
            {
                return OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.AdminRoleNotFound);
            }

            var identityResult = await _userManager.AddToRoleAsync(user, "admin");
            if (!identityResult.Succeeded)
            {
                return identityResult.Errors.Any(e => e.Code is nameof(IdentityErrorDescriber.UserAlreadyInRole))
                    ? OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.UserAlreadyAdmin)
                    : OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.UnknownError);
            }

            await _userManager.RemoveFromRoleAsync(user, "user");

            return OperationResult<GiveAdminRightsError>.Success;
        }

        public async Task<OperationResult<RevokeAdminRightsError>> RevokeAdminRights(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return OperationResult<RevokeAdminRightsError>.Failed(RevokeAdminRightsError.UserNotFound);
            }

            var isUserRoleExist = await _roleRepository.AnyAsync(r => r.NormalizedName == "USER");
            if (!isUserRoleExist)
            {
                return OperationResult<RevokeAdminRightsError>.Failed(RevokeAdminRightsError.UserRoleNotFound);
            }

            var identityResult = await _userManager.RemoveFromRoleAsync(user, "admin");
            if (!identityResult.Succeeded)
            {
                return identityResult.Errors.Any(e => e.Code is nameof(IdentityErrorDescriber.UserNotInRole))
                    ? OperationResult<RevokeAdminRightsError>.Failed(RevokeAdminRightsError.UserNotAdmin)
                    : OperationResult<RevokeAdminRightsError>.Failed(RevokeAdminRightsError.UnknownError);
            }

            await _userManager.AddToRoleAsync(user, "user");

            return OperationResult<RevokeAdminRightsError>.Success;
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
