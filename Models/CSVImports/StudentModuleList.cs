using System;
using CsvHelper.Configuration;

namespace SITSData.Models.CSVImports
{
    public class StudentModuleList
    {
        public int StudentId { get; set; }
        public int? UoPId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Group { get; set; }
        public int? SectionId { get; set; }
    }

    public class StudentModuleListMap : ClassMap<StudentModuleList>
    {
        public StudentModuleListMap()
        {
            Map(m => m.StudentId).Name("Student ID");
            Map(m => m.UoPId).Name("UOP ID");
            Map(m => m.FullName).Name("Student Name");
            Map(m => m.Department).Name("Department");
            Map(m => m.Group).Name("Group");
            Map(m => m.SectionId).Name("SectionID");
        }
    }
}

