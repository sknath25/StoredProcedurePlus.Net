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
            var SpReturn = configuration.CanReturn<ResourceSummary>();
            SpReturn.Maps(v=>v.EmployerDistrict).HasParameterName("EmployerDisctrict");
            SpReturn.Maps(v => v.MobileNo).HasParameterName("ModileNo");
        }
    }
}
