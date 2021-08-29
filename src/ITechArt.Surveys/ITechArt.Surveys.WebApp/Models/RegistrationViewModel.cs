using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.Surveys.WebApp.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(2, ErrorMessage = "Username should contain 2-30 characters")]
        [MaxLength(30, ErrorMessage = "Username should contain 2-30 characters")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password should contain 6-30 characters")]
        [MaxLength(30, ErrorMessage = "Password should contain 6-30 characters")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and the confirmation password do not match.")]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
