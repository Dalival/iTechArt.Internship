using System;
using System.Collections.Generic;

namespace ITechArt.Surveys.DomainModel
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
