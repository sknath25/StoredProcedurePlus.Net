using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.UnitTests.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.StoredProcedureManagers.UnitTests
{
    [TestClass()]
    public class StoredProcedureManagerTests
    {
        static string Hash = Guid.NewGuid().ToString();

        [TestMethod()]
        public void TakesTest()
        {
            MyFirstStoredProcedure p1 = new MyFirstStoredProcedure();
            MyFirstStoredProcedure p2 = new MyFirstStoredProcedure();
            MyFirstStoredProcedure p3 = new MyFirstStoredProcedure();
            MyFirstStoredProcedure p4 = new MyFirstStoredProcedure();

            School Entity = new School() { SchoolName = "Bhadrakali", SchoolId=30 };
            School Entity1 = new School() { SchoolName = "l" };

            p1.Execute(Entity);
            p1.Execute(Entity1);
        }

        [TestMethod()]
        public void ResourceSummaryTest()
        {
            ResourceSummary Input = new ResourceSummary()
            {
                PersonName="SQL++.NET LIB PERFORMANCE TEST",
                Country="INDIA",
                State="GUJRAT",
                City="BARODA",
                District="UNSPECIFIED",
                Street="1 SHANTI NAGAR STREET",
                HouseNo="C8",
                Pin = Hash,
                MobileNo = "9051778445",
                HomePhoneNo = "9051778445",
                EmailAddress = "cdiprod.visualstudio@cdicorp.com",
                MothersName = "VISUAL STUDIO",
                FathersName = "PERFORMANCE TEST",
                Employer = "COP",
                EmployerCountry="USA",
                EmployerCity= "BALA CYNWYD",
                EmployerState = "PENNSYLVANIA",
                EmployerDistrict = "PHILADELPHIA",                
                EmployerPin="19015",
                EmployerStreet="1800 GREEN STREET",
                EmployerHouseNo="F2",
                CTC = 1000.56M,
                NET=10000.65M,
                Gross = 10000.11M,   
                MobileNo2= Hash
            };

            SpResourceSummary Sp = new SpResourceSummary();
            Sp.Execute(Input);
            Console.Write(Input.PersonId);

            Assert.IsTrue(Input.PersonId > 0);
        }

        [TestMethod()]
        public void ResourceSummaryTest2()
        {
            Resource Input = new Resource()
            {
               id=90, name="Debajyoti" 
            };

            SpResourceSummary2 Sp = new SpResourceSummary2();

            Sp.Execute(Input);

            //Assert.IsTrue(Input.PersonId > 0);
        }
    }
}