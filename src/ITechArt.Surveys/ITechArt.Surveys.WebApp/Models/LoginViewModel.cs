using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.Surveys.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter an email or a username")]
        [DisplayName("Email or username")]
        public string EmailOrUserName { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
