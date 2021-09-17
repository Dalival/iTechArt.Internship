using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Foundation.Model;
using ITechArt.Surveys.Foundation.Result;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation
{
    public class UserService : IUserService
    {
        private readonly ICustomLogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<User> _userRepository;


        public UserService(ICustomLogger logger, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;

            _userRepository = _unitOfWork.GetRepository<User>();
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

        public async Task<IReadOnlyCollection<UserDataForTable>> GetUsersShortDataAsync()
        {
            var users = await _userRepository.GetAllAsync(u => u.UserRoles);
            var usersForTable = users.Select(u => new UserDataForTable
                {
                    Name = u.UserName,
                    Role = u.UserRoles.Single().Role.Name,
                    RegistrationDate = u.RegistrationDate
                })
                .ToList();

            return usersForTable;
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
                    nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars) => RegistrationError
                        .PasswordRequiresMoreUniqueChars,
                    nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => RegistrationError
                        .PasswordRequiresNonAlphanumeric,
                    _ => RegistrationError.UnknownError
                })
                .ToList();

            var operationResult = OperationResult<RegistrationError>.Failed(registrationErrors);

            return operationResult;
        }
    }
}
