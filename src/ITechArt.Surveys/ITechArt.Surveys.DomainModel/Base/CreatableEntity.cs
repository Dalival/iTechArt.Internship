using System;

namespace ITechArt.Surveys.DomainModel.Base
{
    public abstract class CreatableEntity : Entity
    {
        public DateTime CreationDate { get; set; }

        public string CreatedById { get; set; }

        public User CreatedBy { get; set; }
    }
}
