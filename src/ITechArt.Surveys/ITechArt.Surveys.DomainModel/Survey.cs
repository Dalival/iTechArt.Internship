using System;
using System.Collections.Generic;
using ITechArt.Repositories;

namespace ITechArt.Surveys.DomainModel
{
    public class Survey : BaseModel
    {
        public string Name { get; set; }
        
        public DateTime EditTime { get; set; }
        
        public virtual List<Section> Sections { get; set; }
    }
}
