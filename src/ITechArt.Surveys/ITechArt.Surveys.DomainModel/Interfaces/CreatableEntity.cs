using System;

namespace ITechArt.Surveys.DomainModel.Interfaces
{
    public abstract class CreatableEntity : Entity
    {
        public DateTime CreationDate { get; set; }

        public string CreatedById { get; set; }

        public User CreatedBy { get; set; }
    }
}
