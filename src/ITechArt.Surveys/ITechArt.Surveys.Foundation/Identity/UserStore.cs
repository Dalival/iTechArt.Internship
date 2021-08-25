using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation.Identity
{
    public class UserStore : IUserPasswordStore<User>, IUserEmailStore<User>, IUserRoleStore<User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;


        public UserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<User>();
            _userRoleRepository = unitOfWork.GetRepository<UserRole>();
            _roleRepository = unitOfWork.GetRepository<Role>();
        }


        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _userRepository.Add(user);
                await _unitOfWork.SaveAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Could not add user {user.Email}"
                });
            }
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _userRepository.Update(user);
                await _unitOfWork.SaveAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Could not update user {user.Email}"
                });
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _userRepository.Delete(user);
                await _unitOfWork.SaveAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Could not delete user {user.Email}"
                });
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedName == null)
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (!Guid.TryParse(userId, out var idGuid))
            {
                throw new ArgumentException("Not a valid Guid id", nameof(userId));
            }

            return await _userRepository.GetByIdAsync(idGuid);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedUserName == null)
            {
                throw new ArgumentNullException(nameof(normalizedUserName));
            }

            return await _userRepository.GetSingleOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            user.Email = email;

            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool isConfirmed, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.EmailConfirmed = isConfirmed;

            return Task.CompletedTask;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedEmail == null)
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            return await _userRepository.GetSingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedEmail == null)
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            user.NormalizedEmail = normalizedEmail;

            return Task.CompletedTask;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var targetRole = await _roleRepository.GetSingleOrDefaultAsync(r => r.Name == roleName);

            _userRoleRepository.Add(new UserRole
            {
                RoleId = targetRole.Id,
                UserId = user.Id
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var targetRole = await _roleRepository.GetSingleOrDefaultAsync(r => r.Name == roleName);

            _userRoleRepository.Delete(new UserRole
            {
                RoleId = targetRole.Id,
                UserId = user.Id
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userRolesOfTargetUser = await _userRoleRepository
                .GetWhereAsync(ur => ur.UserId == user.Id, ur => ur.Role);
            var rolesOfTargetUser = userRolesOfTargetUser
                .Select(ur => ur.Role.Name)
                .ToList();

            return rolesOfTargetUser;
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            return await _userRoleRepository
                .AnyAsync(ur => ur.UserId == user.Id && ur.Role.Name == roleName,
                    ur => ur.Role);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var targetUserRoles = await _userRoleRepository
                .GetWhereAsync(ur => ur.Role.Name == roleName,
                    ur => ur.Role, ur => ur.User);

            //TODO: probably ur.User will be null
            return targetUserRoles.Select(ur => ur.User).ToList();
        }

        public void Dispose()
        {
        }
    }
}
