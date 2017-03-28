using StoredProcedurePlus.Net.StoredProcedureManagers;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTests.StoredProcedureManagers
{
    public class MyFirstStoredProcedure:StoredProcedureManager<School>
    {
        protected override void Setup()
        {
            ProcedureName = "SomeSPName";
           
            InputConfiguration.Maps(v => v.SchoolId).Max(100).HasParameterName("Hamba");
            InputConfiguration.Maps(v => v.SchoolName).Required();
        }
    }
}
