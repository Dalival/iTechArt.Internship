using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysDbContext : DbContext
    {
        public DbSet<Counter> Counters { get; }

        
        public SurveysDbContext(DbContextOptions options)
            : base(options) { }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
