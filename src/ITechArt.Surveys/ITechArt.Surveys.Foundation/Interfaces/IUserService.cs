using System.Threading.Tasks;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsEmailExistAsync(string email);

        Task<bool> IsUserNameExistAsync(string email);

        Task CreateUserAsync(string userName, string email, string password);
    }
}
