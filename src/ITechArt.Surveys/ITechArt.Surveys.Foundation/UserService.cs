using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.Foundation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomLogger _logger;
        private readonly UserManager<User> _userManager;

        public UserService(ICustomLogger logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user != null;
        }

        public async Task CreateUserAsync(string userName, string email, string password)
        {
            if (password == null)
            {

            }
            var user = new User
            {
                UserName = userName,
                Email = email
            };
            await _userManager.CreateAsync(user, password);
        }
    }
}
