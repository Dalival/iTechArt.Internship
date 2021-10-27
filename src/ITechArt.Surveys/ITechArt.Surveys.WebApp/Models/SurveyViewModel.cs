using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.Surveys.WebApp.Models
{
    public class SurveyViewModel
    {
        [Required(ErrorMessage = "Set survey name")]
        [MinLength(2, ErrorMessage = "Name should contain 2-256 characters")]
        [MaxLength(256, ErrorMessage = "Name should contain 2-256 characters")]
        [DisplayName("Name")]
        public string Name { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
