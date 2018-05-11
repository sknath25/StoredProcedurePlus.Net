using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StoredProcedurePlus.Net.UnitTests.RealSituationTests
{
    [TestClass]
    public class OvsDbTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void SP_SuggestedWorkflow_TEST()
        {
            List<SP_SuggestedWorkflowResultType> Items = null;
            for (int i = 0; i < 4000; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                SP_SuggestedWorkflow SP = new SP_SuggestedWorkflow();
                SP.Execute(null);
                watch.Stop();
                TestContext.WriteLine("Call time : " + watch.ElapsedMilliseconds);
                watch.Reset();
                watch.Start();
                Items =
                    SP.GetResult<SP_SuggestedWorkflowResultType>().ToList();
                watch.Stop();
                TestContext.WriteLine("Cast time : " + watch.ElapsedMilliseconds);
            }

            Assert.IsTrue(Items != null && Items.Count > 0);
        }

        [TestMethod]
        public void SP_SuggestedWorkflow_ADOTEST()
        {
            List<SP_SuggestedWorkflowResultType> Items = null;

            Type t = typeof(SP_SuggestedWorkflowResultType);
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, MethodInfo> namesetters = new Dictionary<string, MethodInfo>();

            foreach (PropertyInfo pi in props)
            {
                namesetters.Add(pi.Name, pi.GetSetMethod());
            }

            for (int i = 0; i < 4000; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Items = new List<SP_SuggestedWorkflowResultType>();
                SqlConnection conn = new SqlConnection("Persist Security Info=False;User ID=sa;Password=Reset123;Initial Catalog=OVSdb;Server=10.12.1.161");
                conn.Open();
                SqlCommand com = new SqlCommand("SP_SuggestedWorkflow");
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    SP_SuggestedWorkflowResultType item = new SP_SuggestedWorkflowResultType();

                    foreach(var keyvalue in namesetters)
                    {
                        int ord = reader.GetOrdinal(keyvalue.Key);
                        keyvalue.Value.Invoke(item, new object[] { reader.GetValue(ord) });
                    }

                    Items.Add(item);
                }
                com.Dispose();
                conn.Dispose();

                watch.Stop();
                TestContext.WriteLine("Call time : " + watch.ElapsedMilliseconds);
            }

            Assert.IsTrue(Items != null && Items.Count > 0);
        }

        [TestMethod]
        public void Sp_campaignListTest()
        {
            sp_campaign_select_paramters p = new sp_campaign_select_paramters()
            {
                PageNo = 0,
                RecordPerPage = 100,
                CampaignNameContains = "Test"
            };

            sp_campaign_select sp = new sp_campaign_select();
            sp.Execute(p);
            List<sp_campaign_select_result_type> rt = sp.GetResult<sp_campaign_select_result_type>().ToList();
        }

    }
}
