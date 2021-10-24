using System.Collections.Generic;

namespace ITechArt.Surveys.DomainModel
{
    public class Section
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberInSurvey { get; set; }

        public Survey Survey { get; set; }

        public List<Question> Questions { get; set; }
    }
}
