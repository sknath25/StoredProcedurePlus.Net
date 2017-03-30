using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTests.StoredProcedures
{
    public class SpResourceSummary : StoredProcedureManager<ResourceSummary>
    {
        protected override void Setup(ProcedureConfiguration<ResourceSummary> configuration)
        {
            configuration.ProcedureName = "InsertResourceSummary";
            configuration.ConnectionString = "Data Source=PIS03CDIVDISS33;Initial Catalog=PerformanceTestDb;Integrated Security=True;Connection Timeout=60;Min Pool Size=20; Max Pool Size=500;";
            configuration.Input.Maps(v => v.PersonId).Out();
            configuration.Input.Maps(v => v.PersonName).Required().MinLength(5).MaxLength(50);
            configuration.Input.Maps(v => v.EmailAddress).Required().MaxLength(50);
            configuration.Input.Maps(v => v.MobileNo).Required().MaxLength(50);
            configuration.Input.Maps(v => v.Country).MaxLength(50);
        }
    }
}
