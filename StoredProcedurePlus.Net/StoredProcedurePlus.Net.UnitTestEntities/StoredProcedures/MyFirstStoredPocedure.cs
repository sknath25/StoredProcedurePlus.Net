﻿using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class MyFirstStoredProcedure : StoredProcedureManager<SchoolType>
    {
        protected override void Setup(ProcedureConfiguration<SchoolType> configuration)
        {
            configuration.ProcedureName = "SomeSPName";
            configuration.Input.Maps(v => v.SchoolId).Max(2).HasParameterName("Hamba");
            configuration.Input.Maps(v => v.SchoolName).Required();
            configuration.Input.Maps(v => v.SType).AllowedOnly(new short[] { 1, 5, 11, 13 });

            //configuration.Input.Maps(v => v.Students).HasListTypeof<Student>().Maps(v=>v.StudentId);

            //configuration.Output<School>.Maps(v => v.SchoolName).Required();
        }
    }

    public class SPInsertUniversity : StoredProcedureManager<SPInsertUniversity, University>
    {
        protected override void Setup(ProcedureConfiguration<University> configuration)
        {
            configuration.Input.Maps(v => v.UniversityId).Out();
            configuration.Input.Maps(v => v.UniversityName).Required();
            configuration.Input.Maps(v => v.UniversityType).AllowedOnly(new int[] { 1, 2, 3 });
            var schoolconfig = configuration.Input.MapAsTable(v => v.Schools);
            schoolconfig.Maps(v => v.SchoolName).Required();
            schoolconfig.Maps(v => v.SType).AllowedOnly(new short[] { 4, 5, 7}).HasParameterName("SchoolType");
        }
    }
}
