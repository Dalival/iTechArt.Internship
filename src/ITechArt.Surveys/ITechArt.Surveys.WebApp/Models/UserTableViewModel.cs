using System.Collections.Generic;
using ITechArt.Surveys.Foundation.Model;

namespace ITechArt.Surveys.WebApp.Models
{
    public class UserTableViewModel
    {
        public List<UserInTableViewModel> Users { get; set; }

        public int UsersCounter { get; set; }

        public UserTableViewModel(List<UserDataForTable> userDataForTable)
        {
            Users = new List<UserInTableViewModel>();

            foreach (var user in userDataForTable)
            {
                Users.Add(new UserInTableViewModel
                {
                    Name = user.Name,
                    RegistrationDate = user.RegistrationDate,
                    Role = user.Role
                });
            }

            UsersCounter = Users.Capacity;
        }
    }
}
