using ITechArt.Repositories;

namespace ITechArt.Surveys.DomainModel
{
    public class Counter : IDbModel
    {
        public int Id { get; set; }
        
        public int Value { get; set; }
    }
}
