using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.DomainModel
{
    public class Role : IdentityRole
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
