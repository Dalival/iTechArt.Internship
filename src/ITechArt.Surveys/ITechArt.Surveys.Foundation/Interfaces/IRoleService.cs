using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IRoleService
    {
        Task<IReadOnlyCollection<Role>> GetAllRolesAsync();
    }
}
