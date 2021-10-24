namespace ITechArt.Surveys.DomainModel
{
    public class Choice
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public Question Question { get; set; }
    }
}
