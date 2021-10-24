namespace ITechArt.Surveys.DomainModel.Responses
{
    public class SingleChoiceResponse
    {
        public string Id { get; set; }

        public Choice Choice { get; set; }

        public Response Response { get; set; }

        public Question Question { get; set; }
    }
}
