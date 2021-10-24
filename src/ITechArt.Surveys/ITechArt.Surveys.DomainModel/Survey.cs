using System.Collections.Generic;
using ITechArt.Surveys.DomainModel.Responses;

namespace ITechArt.Surveys.DomainModel
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public User Owner { get; set; }

        public List<Section> Sections { get; set; }

        public List<Response> Responses { get; set; }
    }
}
