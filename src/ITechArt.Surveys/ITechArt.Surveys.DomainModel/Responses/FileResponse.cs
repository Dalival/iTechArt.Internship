namespace ITechArt.Surveys.DomainModel.Responses
{
    public class FileResponse
    {
        public string Id { get; set; }

        public string FilePath { get; set; }

        public Response Response { get; set; }

        public Question Question { get; set; }
    }
}
