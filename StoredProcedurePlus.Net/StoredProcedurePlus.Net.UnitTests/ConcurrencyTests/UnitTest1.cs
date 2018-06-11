using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTests.ConcurrencyTests
{
    public class spGetItemsParams
    {
        public int Category_Id { get; set; }
    }

    public class spGetItemsResult
    {
        public string ItemName { get; set; }
        public string Category_Name { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Author { get; set; }
        public string Item_img_Path { get; set; }
        public int price { get; set; }
        public string Notes { get; set; }
        public string View_Buy_Link { get; set; }
    }

    public class spGetItems : StoredProcedureManager<spGetItems, spGetItemsParams>
    {
        protected override void Setup(ProcedureConfiguration<spGetItemsParams> configuration)
        {
            configuration.ConnectionStringName = "information_db";
            configuration.CanReturnCollectionOf<spGetItemsResult>();
        }
    }

    public class spGetCategoryResult
    {
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
    }

    public class spGetCategory : StoredProcedureManager<spGetCategory, spGetItemsParams>
    {
        protected override void Setup(ProcedureConfiguration<spGetItemsParams> configuration)
        {
            configuration.ConnectionStringName = "information_db";
            configuration.CanReturnCollectionOf<spGetCategoryResult>();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            spGetItemsParams param = new spGetItemsParams()
            {
                Category_Id = 0
            };
            spGetCategory sp = new spGetCategory();
            sp.Execute(param);
            List<spGetCategoryResult> rs1 = sp.GetResult<spGetCategoryResult>().ToList();

            spGetItemsParams param1 = new spGetItemsParams()
            {
                Category_Id = 0
            };
            spGetItems sp1 = new spGetItems();
            sp1.Execute(param1);
            List<spGetItemsResult> rs2 = sp1.GetResult<spGetItemsResult>().ToList();
        }
    }
}
