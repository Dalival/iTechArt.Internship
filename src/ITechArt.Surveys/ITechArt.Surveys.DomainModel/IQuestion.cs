namespace ITechArt.Surveys.DomainModel
{
    public interface IQuestion
    {
        public string Label { get; set; }
        
        public bool IsNecessary { get; set; }
        
        public Section Section { get; set; }
    }
}