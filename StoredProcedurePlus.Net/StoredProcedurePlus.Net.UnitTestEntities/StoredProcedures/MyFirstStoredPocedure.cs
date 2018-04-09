using StoredProcedurePlus.Net.StoredProcedureManagers;

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

    public class SP_Insert_Event_Workflow_ActivityTransaction : StoredProcedureManager<
   SP_Insert_Event_Workflow_ActivityTransaction,
   SP_Insert_Event_Workflow_ActivityTransactionParamters>
    {
        protected override void Setup(ProcedureConfiguration<SP_Insert_Event_Workflow_ActivityTransactionParamters> configuration)
        {
            //configuration.Mock = true; // Enable to mock
            configuration.ConnectionString = "Persist Security Info=False;User ID=sa;Password=1234;Initial Catalog=OVSdb;Server=localhost";
            configuration.Input.Maps(v => v.OutStatusID).Out();
            var EventActivityDetailConfig = configuration.Input.MapAsTable(v => v.EventActivityDetail, "EventActivityTableX");
            //EventActivityDetailConfig.IncludeUnmappedProperties = false;
            //EventActivityDetailConfig.Maps(v => v.MasterWorkflowId);
            //EventActivityDetailConfig.Maps(v => v.EventId);
            //EventActivityDetailConfig.Maps(v => v.ActivityId);
            EventActivityDetailConfig.Maps(v => v.ModifiedDescription).MaxLength(500);
            EventActivityDetailConfig.Maps(v => v.CustomSequence).Min(1);
            EventActivityDetailConfig.Maps(v => v.AssignedToGroupId).Min(1);
            //EventActivityDetailConfig.Maps(v => v.ActivityStartDate);
            //EventActivityDetailConfig.Maps(v => v.ActivityEndDate);
            EventActivityDetailConfig.Maps(v => v.AssignmentTypeId).Min(1);
            EventActivityDetailConfig.Maps(v => v.CreatedBy).Min(1);
            //EventActivityDetailConfig.Maps(v => v.AttachmentName);
            EventActivityDetailConfig.Maps(v => v.AttachmentExtension).MaxLength(5);
            //EventActivityDetailConfig.Maps(v => v.AttachmentContent);
            //EventActivityDetailConfig.Maps(v => v.UploadDate);
            EventActivityDetailConfig.Maps(v => v.AttachedBy).Min(1);
        }
    }
}
