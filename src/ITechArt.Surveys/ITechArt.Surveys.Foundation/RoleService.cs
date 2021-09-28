using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Surveys.Foundation
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;


        public RoleService(ISurveysUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<Role>();
        }


        public async Task<IReadOnlyCollection<Role>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles;
        }
    }
}
