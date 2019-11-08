using System;
using System.Collections.Generic;
using EntityCore.Models.Test;

namespace SITSData.Interfaces
{
    public interface IStudentRepo
    {
        //Returns Success True, Fail False
        public bool AddStudent(Student student);
        public IEnumerable<Student> Students { get; }

        //Upgrade Student to GSD Students
        public bool ChangeToGSDStudent(Student student);
    }
    
}
