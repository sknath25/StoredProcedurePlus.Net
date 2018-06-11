using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class SpResourceSummary2 : StoredProcedureManager<SpResourceSummary2, Resource>
    {
        protected override void Setup(ProcedureConfiguration<Resource> configuration)
        {
            configuration.ProcedureName = "InsertResourceSummary2";
            //configuration.ConnectionString = "Data Source=PIS03CDIVDISS33;Initial Catalog=PerformanceTestDb;Integrated Security=True;";
        }
    }
}
