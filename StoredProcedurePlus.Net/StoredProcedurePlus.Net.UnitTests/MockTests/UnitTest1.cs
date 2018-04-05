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
            sp.OnMockExecution += Sp_OnMockExecution;
            AllTypeParams p = new AllTypeParams() { Id = 1, IsEnabled = true };
            p.Childs = new System.Collections.Generic.List<AllTypeChildParams>();
            p.Childs.Add(new AllTypeChildParams() { Id = 9 });
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
    }
}
