using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.DomainModel
{
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
