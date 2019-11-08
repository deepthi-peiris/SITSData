using System;
using System.Collections.Generic;
using System.Linq;
using EntityCore.Models.Test;
using SITSData.Data;
using SITSData.Interfaces;

namespace SITSData.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly TestDbContext tdbc;

        public StudentRepo(TestDbContext _tdbc)
        {
            tdbc = _tdbc;
        }

        public IEnumerable<Student> Students => tdbc.Students;

        //Us to add a new student record if Id = 0 or student does not exits.
        public bool AddStudent(Student student)
        {
            //if StudentId = 0 Add new Record. For any new GSD or Direct entry Engineering student, this step must be completed.
            if (student.Id == 0 && !IsStudent(student))
            {
                
                Console.WriteLine($"New Student with {student.StudentId} Added");
                tdbc.Students.Add(student);
                tdbc.SaveChanges();
                return true;
            }
            Console.WriteLine($"Student with {student.StudentId} already Exits");
            return false;
            
        }
        //Provde Existing Student
        public bool ChangeToGSDStudent(Student student)
        {
            if (!IsStudent(student)) return false;
            var gsdEntryRecord = new GSDEntryRecord
            {
                StudentId = student.StudentId
            };

            var newrec = (GSDStudent)student;
            newrec.GSDEntryRecord = gsdEntryRecord;
            tdbc.GSDStudents.Update(newrec);
            
            return true;
        }

        private bool IsStudent(Student student)
        {
            
            return (tdbc.Students.FirstOrDefault(s => s.StudentId == student.StudentId) != null);
        }
    }
}
