using System.Collections.Generic;

namespace ITechArt.Surveys.WebApp.Models
{
    public class UserTableViewModel
    {
        public List<UserInTableViewModel> Users { get; set; }

        public int UsersCounter { get; set; }
    }
}
