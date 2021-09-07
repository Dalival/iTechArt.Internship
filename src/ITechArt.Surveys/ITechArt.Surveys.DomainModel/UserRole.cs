using Microsoft.AspNetCore.Identity;

namespace ITechArt.Surveys.DomainModel
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
