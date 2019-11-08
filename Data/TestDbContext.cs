using System;
using EntityCore.Models;
using EntityCore.Models.Admin;
using EntityCore.Models.Exams;
using EntityCore.Models.IELTS;
using EntityCore.Models.LogModels;
using EntityCore.Models.Seating;
using EntityCore.Models.Test;
using Microsoft.EntityFrameworkCore;
using static EntityCore.Models.StudentRegister;

namespace SITSData.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> opt) : base(opt) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=test;User Id=sa;Password=Kotte@293");
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        //To implement data migrations this base constructor is needed.
        public TestDbContext(){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("test");
            modelBuilder.ApplyConfiguration(new StudentConfiguration());

        }


        public DbSet<Student> Students { get; set; }
        public DbSet<GSDStudent> GSDStudents { get; set; }
        public DbSet<EngStudent> EngStudents { get; set; }
        public DbSet<GSDEntryRecord> GSDEntryRecords { get; set; }


    }
}
