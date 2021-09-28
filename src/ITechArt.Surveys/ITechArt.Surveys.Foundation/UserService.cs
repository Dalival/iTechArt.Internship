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
            var resultOfAdd = await AddToRoleAsync(userId, "admin");
            if (!resultOfAdd.Succeeded)
            {
                var result = resultOfAdd.Errors.Single() switch
                {
                    AddToRoleError.UserNotFound => OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.UserNotFound),
                    AddToRoleError.RoleNotFound => OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.AdminRoleNotFound),
                    AddToRoleError.UserAlreadyInRole => OperationResult<GiveAdminRightsError>.Failed(GiveAdminRightsError.UserIsAlreadyAdmin),
                    _ => throw new ArgumentOutOfRangeException()
                };

                return result;
            }

            await RemoveFromRoleAsync(userId, "user");

            return OperationResult<GiveAdminRightsError>.Success;
        }

        public async Task<OperationResult<RevokeAdminRightsError>> RevokeAdminRights(string userId)
        {
            var resultOfAdd = await RemoveFromRoleAsync(userId, "admin");
            if (!resultOfAdd.Succeeded)
            {
                var result = resultOfAdd.Errors.Single() switch
                {
                    RemoveFromRoleError.UserNotFound => OperationResult<RevokeAdminRightsError>.Failed(RevokeAdminRightsError.UserNotFound),
                    RemoveFromRoleError.UserNotInRole => OperationResult<RevokeAdminRightsError>.Failed(RevokeAdminRightsError.UserNotAnAdmin),
                    _ => throw new ArgumentOutOfRangeException()
                };

                return result;
            }

            await AddToRoleAsync(userId, "user");

            return OperationResult<RevokeAdminRightsError>.Success;
        }


        private async Task<OperationResult<AddToRoleError>> AddToRoleAsync(string userId, string roleName)
        {
            var isRoleExist = await _roleRepository.AnyAsync(r => r.NormalizedName == roleName.Normalize());
            if (!isRoleExist)
            {
                return OperationResult<AddToRoleError>.Failed(AddToRoleError.RoleNotFound);
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return OperationResult<AddToRoleError>.Failed(AddToRoleError.UserNotFound);
            }

            var identityResult = await _userManager.AddToRoleAsync(user, roleName);
            var operationResult = identityResult.Succeeded
                ? OperationResult<AddToRoleError>.Success
                : OperationResult<AddToRoleError>.Failed(AddToRoleError.UserAlreadyInRole);

            return operationResult;
        }

        private async Task<OperationResult<RemoveFromRoleError>> RemoveFromRoleAsync(string userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return OperationResult<RemoveFromRoleError>.Failed(RemoveFromRoleError.UserNotFound);
            }

            var identityResult = await _userManager.RemoveFromRoleAsync(user, roleName);
            var operationResult = identityResult.Succeeded
                ? OperationResult<RemoveFromRoleError>.Success
                : OperationResult<RemoveFromRoleError>.Failed(RemoveFromRoleError.UserNotInRole);

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
