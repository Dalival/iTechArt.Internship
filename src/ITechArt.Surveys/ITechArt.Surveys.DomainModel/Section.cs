using System.Collections.Generic;
using ITechArt.Repositories;

namespace ITechArt.Surveys.DomainModel
{
    public class Section : BaseModel
    {
        public string Label { get; set; }
        
        public string Description { get; set; }
        
        public virtual Survey Survey { get; set; }
        
        //public virtual <IQuestion> Questions { get; set; }
    }
}