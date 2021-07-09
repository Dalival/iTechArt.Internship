using System;
using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysDbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        
        public DbSet<Section> Sections { get; set; }
        
        // there will be a lot of tables. One table for each type of questions
        public DbSet<IQuestion> Questions { get; set; }
    }
}