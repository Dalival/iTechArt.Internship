using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysDbContext : DbContext
    {
        public SurveysDbContext(DbContextOptions options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Counter>().HasKey(c => c.Id);
        }
    }
}
