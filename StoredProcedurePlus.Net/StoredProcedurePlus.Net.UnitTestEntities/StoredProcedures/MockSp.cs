using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class MockSp : StoredProcedureManager<AllTypeParams>
    {
        protected override void Setup(ProcedureConfiguration<AllTypeParams> configuration)
        {
            configuration.Mock = true;
            configuration.Input.Maps(v => v.Id).Min(1);
            configuration.Input.Maps(v => v.RowChanged).Out();
        }
    }

    public class SpHelloWorldParams
    {
        public string MyName { get; set; }
        public string MyMessage { get; set; }
    }

    public class SpHelloWorld : StoredProcedureManager<SpHelloWorldParams>
    {
        protected override void Setup(ProcedureConfiguration<SpHelloWorldParams> configuration)
        {
            configuration.ConnectionString = "Data Source=.;Initial Catalog=SQLML;Integrated Security=True;";
            configuration.Input.Maps(v => v.MyMessage).Out();
        }
    }
}
