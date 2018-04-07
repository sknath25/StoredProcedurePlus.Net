using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTestEntities
{
    public class AllTypeParams
    {
        public long Id { get; set; } 
        public bool IsEnabled { get; set; }
        public int? RowChanged { get; set; }
        public List<AllTypeChildParams> Childs { get; set; }
    }

    public class AllTypeChildParams
    {
        public long Id { get; set; }
        public bool IsEnabled { get; set; }
        public int? RowChanged { get; set; }
    }

    public class University
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int UniversityType { get; set; }
        public List<School> Schools { get; set; }
    }

    public class School
    {
        public int UniversityId { get; set; }
        public int SchoolId { get; set; }
        public short SchoolType { get; set; }        
        public string SchoolName { get; set; }
        public string SchoolDescription { get; set; }
        public string SchoolAddress { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public int SchoolId { get; set; }
        public short StudentType { get; set; }        
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public string StudentAddress { get; set; }
    }
}
