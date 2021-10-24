namespace ITechArt.Surveys.DomainModel
{
    public class Question
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // for question with numeric response only (e.g. rating, scale)
        public int MaxValue { get; set; }

        public bool Required { get; set; }

        public QuestionType Type { get; set; }

        public int NumberInSection { get; set; }

        public Section Section { get; set; }
    }
}
