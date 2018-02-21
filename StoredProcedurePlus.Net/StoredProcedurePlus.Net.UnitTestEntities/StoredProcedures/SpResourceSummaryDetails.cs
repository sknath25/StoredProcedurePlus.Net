using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class SpResourceSummaryDetails: StoredProcedureManager<ResourceSummary>
    {
        protected override void Setup(ProcedureConfiguration<ResourceSummary> configuration)
        {
            configuration.ProcedureName = "InsertResourceSummaryDetails";
            var SpReturn = configuration.CanReturnCollectionOf<ResourceSummary>();
            SpReturn.Maps(v=>v.EmployerDistrict).HasParameterName("EmployerDisctrict");
            SpReturn.Maps(v => v.MobileNo).HasParameterName("ModileNo");
        }
    }
}
