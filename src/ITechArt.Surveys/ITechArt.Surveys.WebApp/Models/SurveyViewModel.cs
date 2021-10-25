using System.Collections.Generic;

namespace ITechArt.Surveys.WebApp.Models
{
    public class SurveyViewModel
    {
        public string Name { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
