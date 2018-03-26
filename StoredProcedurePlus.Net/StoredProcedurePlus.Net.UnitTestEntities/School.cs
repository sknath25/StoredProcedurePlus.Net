using System.Collections.Generic;

namespace StoredProcedurePlus.Net.UnitTestEntities
{
    public class School
    {

        public short StudentTYpe { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolDescription { get; set; }
        public string SchoolAddress { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public short StudentType { get; set; }
        public int SchoolId { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public string StudentAddress { get; set; }
    }
}
