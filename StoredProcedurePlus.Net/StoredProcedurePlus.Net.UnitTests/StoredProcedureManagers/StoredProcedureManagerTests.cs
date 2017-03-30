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
                Employer = "debajyoti"
                ,
                EmailAddress = "skumar2@cdicorp.com"
                ,
                CTC = 1000.56M
                ,
                Pin = "712233"
                ,
                MobileNo = "9051778445"
            };

            SpResourceSummary Sp = new SpResourceSummary();

            SpResourceSummary Sp2 = new SpResourceSummary();

            Sp.Execute(Input);
            Sp2.Execute(Input);
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