using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.StoredProcedureManagers;
using System.Collections.Generic;
using System.Data;

namespace StoredProcedurePlus.Net.UnitTests.MockTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MockTestSpp()
        {
            MockSp sp = new MockSp();
            sp.OnMockExecutionEventHandler += Sp_OnMockExecution;
            AllTypeParams p = new AllTypeParams() { Id = 1, IsEnabled = true };
            p.Childs = new System.Collections.Generic.List<AllTypeChildParams>();
            p.Childs.Add(new AllTypeChildParams() { Id = 11 });
            sp.Execute(p);

            Assert.IsTrue(p.IsEnabled);
        }

        private void Sp_OnMockExecution(object sender, MockEventArgs args)
        {
            long id = args.Input.GetLong(args.Input.GetOrdinal("Id"));

            DataTable alurdom = args.Input.GetTable(args.Input.GetOrdinal("Childs"));

            if (id > 0)
            {
                args.Input.SetBool(args.Input.GetOrdinal("IsEnabled"), true);
                args.Input.SetInt(args.Input.GetOrdinal("RowChanged"), 19);
            }

            args.Result = 1;
        }

        [TestMethod]
        public void N2LayerMockTest()
        {
            UniversityMockSp sp = new UniversityMockSp();
            sp.OnMockExecutionEventHandler += Sp_OnMockExecution1;
            University pu = new University();
            pu.UniversityType = 3;
            pu.UniversityName = "UNI 1";
            pu.Schools = CreateSchools();
            
            sp.Execute(pu);
        }

        List<SchoolType> CreateSchools()
        {
            List<SchoolType> schools = new List<SchoolType>();

            for (int i = 0; i <= 100; i++)
            {
                SchoolType school = new SchoolType()
                {
                    SchoolName = "SCHOOL : " + i.ToString(),
                    SType = (short)new Random(4).Next(6),
                    Students = CreateStudents()
                };

                schools.Add(school);
            }
            return schools;
        }

        List<Student> CreateStudents()
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i <= 100; i++ )
            {
                Student std = new Student()
                {
                    StudentName = "SUMAN : " + i.ToString(),
                    StudentType = 8
                };
                students.Add(std);    
            }
            return students;
        }

        private void Sp_OnMockExecution1(object sender, MockEventArgs e)
        {
            string un = e.Input.GetString(e.Input.GetOrdinal("UniversityName"));

            DataTable alurdom = e.Input.GetTable(e.Input.GetOrdinal("Schools"));

            e.Result = 1;
        }
    }
}
