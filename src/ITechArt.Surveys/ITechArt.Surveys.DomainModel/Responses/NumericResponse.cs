namespace ITechArt.Surveys.DomainModel.Responses
{
    public class NumericResponse
    {
        public string Id { get; set; }

        public int Value { get; set; }

        public Response Response { get; set; }

        public Question Question { get; set; }
    }
}
