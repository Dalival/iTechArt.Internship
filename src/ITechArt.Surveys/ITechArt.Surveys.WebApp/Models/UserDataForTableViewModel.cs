using System;
using System.Collections.Generic;

namespace ITechArt.Surveys.WebApp.Models
{
    public class UserDataForTableViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
