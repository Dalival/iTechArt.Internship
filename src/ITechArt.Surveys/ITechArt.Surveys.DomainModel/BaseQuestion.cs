using ITechArt.Repositories;

namespace ITechArt.Surveys.DomainModel
{
    public class BaseQuestion : BaseModel, IQuestion
    {
        public string Label { get; set; }
        
        public bool IsNecessary { get; set; }
        
        public Section Section { get; set; }
    }
}