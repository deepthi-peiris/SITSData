using System;
using EntityCore.Models;
using EntityCore.Models.Admin;
using EntityCore.Models.Audits;
using EntityCore.Models.DataImports;
using EntityCore.Models.Exams;
using EntityCore.Models.IELTS;
using EntityCore.Models.LogModels;
using EntityCore.Models.Seating;
using Microsoft.EntityFrameworkCore;

namespace SITSData.Data
{
    public class ExamsDbContext : DbContext
    {
        public ExamsDbContext(DbContextOptions<ExamsDbContext> opt) : base(opt) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=boe;User Id=sa;Password=Kotte@293");
            //optionsBuilder.EnableSensitiveDataLogging();
            
        }

        //To implement data migrations this base constructor is needed.
        public ExamsDbContext(){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("exams");
            modelBuilder.ApplyConfiguration(new CMPModuleMarksConfiguration());
            //modelBuilder.ApplyConfiguration(new IELTSExternalConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleMarksConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleMarkChangeLogConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new DesignationConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TitleConfiguration());
            modelBuilder.ApplyConfiguration(new PathwayConfiguration());
            modelBuilder.ApplyConfiguration(new MCChangeLogConfiguration());
            modelBuilder.ApplyConfiguration(new StudentModuleRegisterConfiguration());

        }

        public DbSet<CMPModuleMark> CMPModuleMarks { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleMark> ModuleMarks { get; set; }
        public DbSet<ModuleMarkChangeLog> ModuleMarkChangeLogs { get; set; }
        //public DbSet<IELTSResults> IELTSResults { get; set; }
        public DbSet<GSDSeating> GSDSeatings { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Pathway> Pathways { get; set; }
        public DbSet<MCChangeLog> MCChangeLogs { get; set; }
        public DbSet<StudentModuleRegister> StudentModuleRegisters { get; set; }




    }
}
