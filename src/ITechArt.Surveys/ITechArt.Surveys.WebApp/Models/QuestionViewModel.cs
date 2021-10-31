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

        [MaxLength(5000, ErrorMessage = "Description max length is 5000 characters")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public int Index { get; set; }
    }
}
