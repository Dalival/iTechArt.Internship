using System.Collections.Generic;

namespace ITechArt.Surveys.DomainModel.Responses
{
    public class MultipleChoiceResponse
    {
        public string Id { get; set; }

        public List<Choice> Choices { get; set; }

        public Response Response { get; set; }

        public Question Question { get; set; }
    }
}
