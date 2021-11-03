using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.DomainModel
{
    public class User : IdentityUser
    {
        public ICollection<UserRole> UserRoles { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Survey> Surveys { get; set; }
    }
}
