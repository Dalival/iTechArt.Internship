namespace ITechArt.Surveys.DomainModel.Responses
{
    public class SingleOptionResponse
    {
        public string Id { get; set; }

        public Option Option { get; set; }

        public Response Response { get; set; }

        public Question Question { get; set; }
    }
}
