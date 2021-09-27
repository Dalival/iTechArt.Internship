using System.Collections.Generic;

namespace ITechArt.Surveys.WebApp.Models
{
    public class UserTableViewModel
    {
        public IEnumerable<UserDataForTableViewModel> Users { get; set; }

        public int Page { get; set; }

        public int TotalUsersAmount { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
