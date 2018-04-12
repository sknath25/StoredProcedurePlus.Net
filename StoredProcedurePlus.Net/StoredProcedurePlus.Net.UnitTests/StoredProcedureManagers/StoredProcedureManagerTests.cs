using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StoredProcedurePlus.Net.StoredProcedureManagers.UnitTests
{
    [TestClass()]
    public class StoredProcedureManagerTests
    {
        static string Hash = Guid.NewGuid().ToString();

        [TestMethod()]
        public void MockTest()
        {
            MyFirstStoredProcedure p1 = new MyFirstStoredProcedure();
            MyFirstStoredProcedure p2 = new MyFirstStoredProcedure();

            SchoolType Entity = new SchoolType() { SchoolName = "Bhadrakali", SchoolId = 30 };
            SchoolType Entity1 = new SchoolType() { SchoolName = "l" };

            p1.Execute(Entity);
            p1.Execute(Entity1);
        }


        [TestMethod]
        public void SP_Insert_Event_Workflow_ActivityTransaction_TEST()
        {
            SP_Insert_Event_Workflow_ActivityTransactionParamters p = new SP_Insert_Event_Workflow_ActivityTransactionParamters();
            p.EventActivityDetail = new List<EventActivityInsertTable>();
            p.CategoryId = 1;
            p.WorkflowId = 1;
            p.CreatedBy = 1;

            p.EventActivityDetail.Add(new EventActivityInsertTable()
            {
                AssignedToGroupId = 1,
                AttachedBy = 1,
                AssignmentTypeId = 1,
                AttachmentExtension = "doc",
                CreatedBy = 1,
                AttachmentName = "SomeFile",
                CustomSequence = 1,
                AttachmentContent = new byte[10]
            });

            p.EventActivityDetail.Add(new EventActivityInsertTable()
            {
                AssignedToGroupId = 1,
                AttachedBy = 1,
                AssignmentTypeId = 1,
                AttachmentExtension = "doc",
                CreatedBy = 1,
                AttachmentName = "SomeFile2",
                CustomSequence = 2,
                AttachmentContent = new byte[10]
            });

            SP_Insert_Event_Workflow_ActivityTransaction sp = new SP_Insert_Event_Workflow_ActivityTransaction();
            sp.Execute(p);
            Assert.IsTrue(p.OutStatusID > 0);
        }

        [TestMethod]
        public void MinimalisticConfigurationTest()
        {
            SpHelloWorldParams param = new SpHelloWorldParams()
            {
                MyName = "SUMAN"
            };
            SpHelloWorld sp = new SpHelloWorld();
            sp.Execute(param);
            string msg = param.MyMessage; // The out parameters will be set in the mapped property and can be used from here.
            Assert.AreEqual<string>("Hello world SUMAN", msg);
        }

        [TestMethod()]
        public void BasicTest()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 1000.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            SpResourceSummary Sp = new SpResourceSummary();

            Sp.Execute(Input);
            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void MultipleInstanceTest()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 100.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            SpResourceSummary[] Multiple = new SpResourceSummary[10];
            for (int i = 0; i < Multiple.Length; i++)
            {
                Multiple[i] = new SpResourceSummary();
            }

            StringBuilder Log = new StringBuilder();
            for (int i = 0; i < Multiple.Length; i++)
            {
                Input.CTC = Input.CTC + 1;
                Multiple[i].Execute(Input);
                Log.AppendLine(string.Format("Person ID : {0}", Input.PersonId));
            }

            Console.Write(Log);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestCloseEach()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 1000.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            using (ConnectionScope scope = new ConnectionScope(ConnectionScopeType.CloseAfterEachExecution))
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestKeepOpen()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 1000.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            using (ConnectionScope scope = new ConnectionScope(ConnectionScopeType.KeepOpen))
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestDisposeEach()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 1000.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            using (ConnectionScope scope = new ConnectionScope(ConnectionScopeType.DisposeAfterEachExecution))
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestDefault()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
                Country = "INDIA",
                State = "GUJRAT",
                City = "BARODA",
                District = "UNSPECIFIED",
                Street = "1 SHANTI NAGAR STREET",
                HouseNo = "C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry = "USA",
                EmployerCity = "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",
                EmployerPin = "19015",
                EmployerStreet = "1800 GREEN STREET",
                EmployerHouseNo = "F2",
                CTC = 1000.56M,
                NET = 10000.65M,
                Gross = 10000.11M,
                MobileNo2 = Hash
            };

            using (ConnectionScope scope = new ConnectionScope())
            {
                SpResourceSummary Sp = new SpResourceSummary();

                for (int i = 0; i < 100; i++)
                {
                    Sp.Execute(Input, scope);
                }
            }

            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ScopeTestRetrival()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName = "SQL++.NET LIB PERFORMANCE TEST",
            };

            IEnumerable<ResourceSummary> Result = null;

            using (ConnectionScope scope = new ConnectionScope())
            {
                SpResourceSummaryDetails Sp = new SpResourceSummaryDetails();

                Stopwatch sw = new Stopwatch();
                sw.Start();

                Sp.Execute(Input, scope);

                Result =
                   Sp.GetResult<ResourceSummary>();

                sw.Stop();

                Console.Write(string.Format("Record Retrived : {0} in time : {1}", Result != null ? Result.Count() : 0, sw.Elapsed.TotalMilliseconds));

            }

            //Assert.IsTrue(Result.Count > 0);
        }

        [TestMethod()]
        public void TableTypeInputTest()
        {
            DataTable dataTable = new DataTable("SampleDataType");
            //we create column names as per the type in DB 
            dataTable.Columns.Add("StudentType", typeof(string));
            dataTable.Columns.Add("StudentName", typeof(string));
            dataTable.Columns.Add("StudentAddress", typeof(string));
            //and fill in some values 
            dataTable.Rows.Add("1", "SUMAN1", "Some adress 1");
            dataTable.Rows.Add("1", "SUMAN2", "Some adress 2");
            dataTable.Rows.Add("1", "SUMAN3", "Some adress 3");

            SqlParameter P_Student = new SqlParameter();
            //The parameter for the SP must be of SqlDbType.Structured 
            P_Student.ParameterName = "@Student";
            P_Student.SqlDbType = System.Data.SqlDbType.Structured;
            P_Student.Value = dataTable;

            SqlParameter P_SchoolType = new SqlParameter("@SchoolType", 1);
            SqlParameter P_SchoolId = new SqlParameter("@SchoolId", null);
            P_SchoolId.Direction = ParameterDirection.Output;
            SqlParameter P_SchoolName = new SqlParameter("@SchoolName", "School 1");
            SqlParameter P_SchoolDescription = new SqlParameter("@SchoolDescription", "My school 1");
            SqlParameter P_SchoolAddress = new SqlParameter("@SchoolAddress", "My school address 1");

            int school_id = 0;

            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=information;Integrated Security=True;"))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_INSERT_SCHOOL", conn))
                {
                    command.Parameters.Add(P_SchoolType);
                    command.Parameters.Add(P_SchoolId);
                    command.Parameters.Add(P_SchoolName);
                    command.Parameters.Add(P_SchoolDescription);
                    command.Parameters.Add(P_SchoolAddress);
                    command.Parameters.Add(P_Student);

                    command.ExecuteNonQuery();

                    school_id = (int)P_SchoolId.Value;
                }
            }

            Assert.IsTrue(school_id > 1);
        }

        [TestMethod]
        public void TableTypeSpTest()
        {
            University pu = new University();
            pu.UniversityType = 3;
            pu.UniversityName = "UNI 1";
            pu.Schools = CreateSchools();

            SPInsertUniversity SP = new SPInsertUniversity();
            SP.Execute(pu);

            int id = pu.UniversityId;

            Assert.IsTrue(pu.UniversityId > 0);
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

            for (int i = 0; i <= 100; i++)
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

    }
}