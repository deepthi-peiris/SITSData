using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityCore.Models.Abstract;
using EntityCore.Models.IELTS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityCore.Models.Test
{
    public enum EnglishLevel
    {
        EL1 = 1,
        EL2 = 2,
        EL3 = 3,
        EL4 = 4,
        EL5 = 5,
        EL6 = 6,
        EL7 = 7
    }

    public class GSDEntryRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }
        //HEAC information
        public int Maths { get; set; }
        public int Physics { get; set; }
        public int English { get; set; }
        //Placement Test Information
        public EnglishLevel? EntryLevel { get; set; }
    }

    public class EngEntryRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }
        public int? BasicMathematics { get; set; }
        public int? PureMathematics { get; set; }
        public int? Physics { get; set; }
        public int? Computing { get; set; }
        public double? BS { get; set; }
        public double? W { get; set; }
        public double? S { get; set; }
        public double? L { get; set; }
        public double? R { get; set; }

        [ForeignKey("IELTSRecord")]
        public int? IELTSResultsId { get; set; }

        //Points to the record in the IELTS Database
        public virtual IELTSResults IELTSRecord { get; set; }
    }

    public class Student : Person, ICloneable
    {
        public int StudentId { get; set; }
        [StringLength(100)]
        public string UnitName { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string Email
        {
            get { return string.Concat(StudentId, "@mtc.edu.om"); }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }

    public class GSDStudent : Student
    {
        public GSDEntryRecord GSDEntryRecord { get; set; }


    }

    public class EngStudent : Student
    {
        public int? UoPId { get; set; }
        public EntryType EntryType { get; set; }
        public EngEntryRecord EngEntryRecord { get; set; }
    }


    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> e)
        {
            e.ToTable("Student", "test");
            e.HasAlternateKey(e => e.StudentId);
        }

    }


    //public class GSDStudentConfiguration : IEntityTypeConfiguration<GSDStudent>
    //{
    //    public void Configure(EntityTypeBuilder<GSDStudent> e)
    //    {
    //        e.ToTable("GSDStudent", "test");
    //        e.HasAlternateKey(e => e.StudentId);
    //    }

    //}

    //public class EngStudentConfiguration : IEntityTypeConfiguration<EngStudent>
    //{
    //    public void Configure(EntityTypeBuilder<EngStudent> e)
    //    {
    //        e.HasAlternateKey(e => e.UoPId);
    //    }

    //}
}
