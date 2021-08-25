using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.Surveys.WebApp.Models
{
    public class RegistrationViewModel
    {
        [MinLength(2), MaxLength(30)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText, MinLength(6), MaxLength(30)]
        public string Password { get; set; }

        [PasswordPropertyText]
        public string RepeatPassword { get; set; }
    }
}
