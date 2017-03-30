using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTests.StoredProcedures
{
    public class MyFirstStoredProcedure:StoredProcedureManager<School>
    {
        protected override void Setup(ProcedureConfiguration<School> configuration)
        {
            configuration.ProcedureName = "SomeSPName";
            configuration.Input.Maps(v => v.SchoolId).Max(200).HasParameterName("Hamba");
            configuration.Input.Maps(v => v.SchoolName).Required();

            //configuration.Output<School>.Maps(v => v.SchoolName).Required();
        }
    }
}
