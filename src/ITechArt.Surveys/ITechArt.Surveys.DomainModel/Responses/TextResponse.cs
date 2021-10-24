﻿namespace ITechArt.Surveys.DomainModel.Responses
{
    public class TextResponse
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public Response Response { get; set; }

        public Question Question { get; set; }
    }
}
