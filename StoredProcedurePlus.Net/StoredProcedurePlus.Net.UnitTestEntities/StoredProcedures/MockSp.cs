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
            configuration.Input.MapAsTable(v => v.Childs, "ChildType").Maps(v => v.Id).Min(10);
            configuration.Input.Maps(v => v.RowChanged).Out();
        }
    }

    public class UniversityMockSp : StoredProcedureManager<University>
    {
        protected override void Setup(ProcedureConfiguration<University> configuration)
        {
            configuration.Mock = true;
            configuration.Input.Maps(v => v.UniversityName).Out();
            configuration.Input.Maps(v => v.UniversityName).Required();
            configuration.Input.Maps(v => v.UniversityType).AllowedOnly(new int[] { 1, 2, 3 });
            var schoolmappar = configuration.Input.MapAsTable(v => v.Schools, "SchoolType");
            schoolmappar.Maps(v => v.SchoolName).Required();
            schoolmappar.Maps(v => v.SchoolType).AllowedOnly(new short[] { 4, 5, 6 });

            //var studentmappar = schoolmappar.MapAsTable(v => v.Students, "StudentType");
            //studentmappar.Maps(v => v.StudentName).Required();
            //studentmappar.Maps(v => v.StudentType).AllowedOnly(new short[] { 7, 8, 9 });
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
            //configuration.ConnectionStringName = "Ami_Kana_Suji";
            //configuration.ConnectionString = "Data Source=.;Initial Catalog=SQLML;Integrated Security=True;";
            configuration.Input.Maps(v => v.MyMessage).Out();
        }
    }

    public class SpHelloWorldScalarResult
    {
        public string MyMessage { get; set; }
    }

    public class SpHelloWorldScalar : StoredProcedureManager<SpHelloWorldParams>
    {
        protected override void Setup(ProcedureConfiguration<SpHelloWorldParams> configuration)
        {
            configuration.ConnectionString = "Data Source=.;Initial Catalog=SQLML;Integrated Security=True;";
            configuration.Input.Maps(v => v.MyMessage).Out();
            configuration.CanReturnCollectionOf<SpHelloWorldScalarResult>();
        }
    }

public class SpHelloWorldQueryResult
{
    public int Id { get; set; }
    public double Sepal_Length { get; set; }
    public double Sepal_Width { get; set; }
    public double Petal_Length { get; set; }
    public double Petal_Width { get; set; }
    public string Species { get; set; }
}

public class SpHelloWorldQuery : StoredProcedureManager<SpHelloWorldParams>
{
    protected override void Setup(ProcedureConfiguration<SpHelloWorldParams> configuration)
    {
        configuration.ConnectionString = "Data Source=.;Initial Catalog=SQLML;Integrated Security=True;";
        configuration.Input.Maps(v => v.MyMessage).Out();
        configuration.CanReturnCollectionOf<SpHelloWorldScalarResult>();
        var config = configuration.CanReturnCollectionOf<SpHelloWorldQueryResult>();

        config.Maps(V => V.Petal_Length).HasParameterName("Petal.Length");
        config.Maps(V => V.Petal_Width).HasParameterName("Petal.Width");
        config.Maps(V => V.Sepal_Length).HasParameterName("Sepal.Length");
        config.Maps(V => V.Sepal_Width).HasParameterName("Sepal.Width");
    }
}
}
