using System;
using CsvHelper.Configuration;

namespace SITSData.Models.CSVImports
{

    public class StudentListView
    {
        public int StudentId { get; set; }
        public int? UoPId { get; set; }
        public int CivilId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Unit { get; set; }
        public string Group { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        //public int MobileNo { get; set; }
        //public DateTime? DOB { get; set; }
        //public string Gender { get; set; }

    }

    public class StudentListViewMap : ClassMap<StudentListView>
    {
        public StudentListViewMap()
        {
            Map(m => m.StudentId).Name("Student ID");
            Map(m => m.UoPId).Name("UOP Number");
            Map(m => m.CivilId).Name("National ID");
            Map(m => m.FullName).Name("English Student Name");
            Map(m => m.Department).Name("Department");
            Map(m => m.Unit).Name("Unit");
            Map(m => m.Group).Name("Group");
            Map(m => m.FirstName).Name("English First Name");
            Map(m => m.SecondName).Name("English Second Name");
            Map(m => m.Surname).Name("English Last Name");
            //Map(m => m.MobileNo).Name("Mobile No");
            //Map(m => m.DOB).Name("Date of Birth").TypeConverterOption.Format("d/M/yyyy");
            //Map(m => m.Gender).Name("Gender");


        }
    }

}

