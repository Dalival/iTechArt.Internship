using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.Surveys.WebApp.Models
{
    public class QuestionViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "Title should contain 2-256 characters")]
        [MaxLength(256, ErrorMessage = "Title should contain 2-256 characters")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Range(0, 500)]
        public int Index { get; set; }
    }
}
