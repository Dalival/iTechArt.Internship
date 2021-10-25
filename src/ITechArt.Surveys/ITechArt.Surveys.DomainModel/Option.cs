namespace ITechArt.Surveys.DomainModel
{
    public class Option
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }

        public Question Question { get; set; }
    }
}
