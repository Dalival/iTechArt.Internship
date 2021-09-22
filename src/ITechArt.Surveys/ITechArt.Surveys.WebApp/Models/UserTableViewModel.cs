using System.Collections.Generic;

namespace ITechArt.Surveys.WebApp.Models
{
    public class UserTableViewModel
    {
        public IEnumerable<UserDataForTable> Users { get; set; }

        public int Page { get; set; }

        public int TotalUsersAmount { get; set; }
    }
}
