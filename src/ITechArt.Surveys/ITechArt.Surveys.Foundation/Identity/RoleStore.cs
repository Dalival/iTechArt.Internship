using System;
using System.Threading;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly ISurveysUnitOfWork _unitOfWork;

        private readonly IRepository<Role> _roleRepository;


        public RoleStore(ISurveysUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _roleRepository = unitOfWork.GetRepository<Role>();
        }


        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            try
            {
                _roleRepository.Add(role);
                await _unitOfWork.SaveAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Could not add user {role.Name}"
                });
            }
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            try
            {
                _roleRepository.Update(role);
                await _unitOfWork.SaveAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Could not update user {role.Name}"
                });
            }
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            try
            {
                _roleRepository.Delete(role);
                await _unitOfWork.SaveAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Could not delete user {role.Name}"
                });
            }
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Role role, string name, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            role.Name = name;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (normalizedName == null)
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (roleId == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            if (!Guid.TryParse(roleId, out var idGuid))
            {
                throw new ArgumentException("Not a valid Guid id", nameof(roleId));
            }

            var role =  await _roleRepository.GetByIdAsync(idGuid);

            return role;
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedRoleName == null)
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            var role = await _roleRepository.GetSingleOrDefaultAsync(r => r.NormalizedName == normalizedRoleName);

            return role;
        }

        public void Dispose()
        {
        }
    }
}
