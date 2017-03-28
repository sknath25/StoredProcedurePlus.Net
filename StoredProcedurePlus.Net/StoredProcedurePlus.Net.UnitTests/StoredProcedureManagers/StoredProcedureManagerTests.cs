using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.UnitTests.StoredProcedureManagers;
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

            IDbConnection Connection = new SqlConnection();

            School Entity = new School();

            p1.Execute(Entity, Connection);

            Assert.Fail();
        }
    }
}