using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace StoredProcedurePlus.Net.UnitTests.DapperTests
{
    [TestClass]
    public class DapperTests1
    {
        //public class NorthwindDatabase : Database
        //{
        //    public Table<ResourceSummary> Suppliers { get; set; }
        //}

            [TestMethod]
        public void DapperRetrivalTest()
        {
            using (var sqlConnection = new SqlConnection("Data Source=PIS03CDIVDISS33;Initial Catalog=PerformanceTestDb;Integrated Security=True;"))
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                sqlConnection.Open();

                IEnumerable<ResourceSummary> resultList = sqlConnection.Query<ResourceSummary>(@"
                    SELECT * 
                    FROM ResourceSummary");


                sw.Stop();

                Console.Write(string.Format("Record Retrived : {0} in time : {1}", resultList != null ? resultList.Count() : 0, sw.Elapsed.TotalMilliseconds));

            }
        }
    }
}
