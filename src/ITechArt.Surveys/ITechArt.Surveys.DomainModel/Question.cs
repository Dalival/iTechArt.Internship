using ITechArt.Surveys.DomainModel.Interfaces;

namespace ITechArt.Surveys.DomainModel
{
    public class Question : Entity
    {
        public string Title { get; set; }

        public int Index { get; set; }

        public string SurveyId { get; set; }

        public Survey Survey { get; set; }
    }
}
