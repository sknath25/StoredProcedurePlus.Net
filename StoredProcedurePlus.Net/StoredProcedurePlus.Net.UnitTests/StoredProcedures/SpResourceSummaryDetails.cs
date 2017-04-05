using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTests.StoredProcedures
{
    public class SpResourceSummaryDetails: StoredProcedureManager<ResourceSummary>
    {
        protected override void Setup(ProcedureConfiguration<ResourceSummary> configuration)
        {
            configuration.ProcedureName = "InsertResourceSummaryDetails";
            configuration.CanReturn<ResourceSummary>();
        }
    }
}
