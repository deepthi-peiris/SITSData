using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCore.Models.Abstract
{
    public abstract class Person
    {
        public Person()
        {
            //Civil Id is a must. Set it to Id value initially.
            CivilId = Id;
        }

        //Establish a Unique Id for the list
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(50)]
        //Civil ID Is a must.  Set it to Student
        public int CivilId { get; set; }
        public int? Phone { get; set; }
        public DateTime? BirthDate { get; set; }

    }

    public enum StudentStatus
    {
        Enrolled = 1,
        Graduated = 2,
        Withdrawn = 3,
        FailedAndExit = 4,
        Suspended = 5,
        NotSet = -1

    }

    public enum EmployeeStatus
    {
        Active = 1,
        Left = 2,
        Suspended = 3,
        NotSet = -1
    }

    public enum EntryType
    {
        Internal = 1,
        DirectEntry = 2,
        NotSet = -1
    }
}
