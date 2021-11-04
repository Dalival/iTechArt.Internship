using System;
using System.Collections.Generic;
using ITechArt.Surveys.DomainModel.Base;

namespace ITechArt.Surveys.DomainModel
{
    public class Survey : CreatableEntity
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
