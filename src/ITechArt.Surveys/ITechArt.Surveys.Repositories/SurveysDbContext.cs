﻿using System;
using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysDbContext : DbContext
    {
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Section> Sections { get; set; }
        // there will be a lot of tables. One table for each type of questions
        //public DbSet<IQuestion> Questions { get; set; }

        public SurveysDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>()
                .HasMany(survey => survey.Sections)
                .WithOne(section => section.Survey);
            
            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
