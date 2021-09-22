using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.DomainModel
{
    public class User : IdentityUser
    {
        public List<UserRole> UserRoles { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
