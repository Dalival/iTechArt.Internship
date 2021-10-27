using System;
using System.Collections.Generic;

namespace ITechArt.Surveys.DomainModel
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public User Owner { get; set; }

        public List<Question> Questions { get; set; }
    }
}
