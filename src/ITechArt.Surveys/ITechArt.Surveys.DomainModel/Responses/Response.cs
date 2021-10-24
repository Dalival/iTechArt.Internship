using System;

namespace ITechArt.Surveys.DomainModel.Responses
{
    public class Response
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public User Respondent { get; set; }

        public Survey Survey { get; set; }
    }
}
