using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.DomainModel
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
