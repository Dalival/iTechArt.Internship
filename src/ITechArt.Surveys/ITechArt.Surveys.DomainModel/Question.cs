using ITechArt.Surveys.DomainModel.Base;

namespace ITechArt.Surveys.DomainModel
{
    public class Question : Entity
    {
        public string Title { get; set; }

        public int Order { get; set; }

        public string SurveyId { get; set; }

        public Survey Survey { get; set; }
    }
}
