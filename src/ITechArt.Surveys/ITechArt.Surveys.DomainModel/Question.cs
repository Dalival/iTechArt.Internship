namespace ITechArt.Surveys.DomainModel
{
    public class Question
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Index { get; set; }

        public QuestionType Type { get; set; }

        public Survey Survey { get; set; }
    }
}
