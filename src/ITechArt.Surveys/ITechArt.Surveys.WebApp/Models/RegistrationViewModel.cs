using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.Surveys.WebApp.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(2, ErrorMessage = "Username should be 2-30 characters"), MaxLength(30, ErrorMessage = "Username should be 2-30 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password), MinLength(6), MaxLength(30)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
    }
}
