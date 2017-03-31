using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTests.StoredProcedures
{
    public class SpResourceSummary2 : StoredProcedureManager<Resource>
    {
        protected override void Setup(ProcedureConfiguration<Resource> configuration)
        {
            configuration.ProcedureName = "InsertResourceSummary2";
            //configuration.ConnectionString = "Data Source=PIS03CDIVDISS33;Initial Catalog=PerformanceTestDb;Integrated Security=True;";
        }
    }
}
