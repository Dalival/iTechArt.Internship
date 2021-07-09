using System.Collections.Generic;

namespace ITechArt.Surveys.DomainModel
{
    public class Section : BaseModel
    {
        public string Label { get; set; }
        
        public string Description { get; set; }
        
        public List<IQuestion> Questions { get; set; }
    }
}