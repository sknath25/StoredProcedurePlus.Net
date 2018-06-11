using StoredProcedurePlus.Net.StoredProcedureManagers;
using System;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class MyFirstStoredProcedure : StoredProcedureManager<MyFirstStoredProcedure, SchoolType>
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
            configuration.ConnectionStringName = "OvsDb";
            configuration.Input.Maps(v => v.OutStatusID).Out();
            configuration.Input.Maps(v => v.EventName).MaxLength(200).Trim();
            configuration.Input.Maps(v => v.EventDescription).MaxLength(500).Trim();
            configuration.Input.Maps(v => v.WorkflowId).Min(1);
            configuration.Input.Maps(v => v.CategoryId).Min(1);
            configuration.Input.Maps(v => v.LocationId).Min(1);
            configuration.Input.Maps(v => v.DepartmentId).Min(1);
            configuration.Input.Maps(v => v.SectionId).Min(1);
            configuration.Input.Maps(v => v.RefEventId).Min(1);
            configuration.Input.Maps(v => v.CreatedBy).Min(1);
            configuration.Input.Maps(v => v.VotingStartDate).Min(DateTime.Today);
            configuration.Input.Maps(v => v.VotingEndDate).Min(DateTime.Today);

            var EventActivityDetailConfig = configuration.Input.MapAsTable(v => v.EventActivityDetail);
            EventActivityDetailConfig.Maps(v => v.MasterWorkflowId).Min(1);
            EventActivityDetailConfig.Maps(v => v.EventId).Min(1);
            EventActivityDetailConfig.Maps(v => v.ActivityId).Min(1);
            EventActivityDetailConfig.Maps(v => v.ModifiedDescription).MaxLength(500).Trim();
            EventActivityDetailConfig.Maps(v => v.CustomSequence).Min(1);
            EventActivityDetailConfig.Maps(v => v.AssignedToGroupId).Min(1);
            EventActivityDetailConfig.Maps(v => v.AssignedToEmailId).Email();
            EventActivityDetailConfig.Maps(v => v.ActivityStartDate).Min(DateTime.Today);
            EventActivityDetailConfig.Maps(v => v.ActivityEndDate).Min(DateTime.Today);
            EventActivityDetailConfig.Maps(v => v.AssignmentTypeId).Min(1);
            EventActivityDetailConfig.Maps(v => v.CreatedBy).Min(1);
            EventActivityDetailConfig.Maps(v => v.AttachmentName).MaxLength(200).Trim();
            EventActivityDetailConfig.Maps(v => v.AttachmentExtension).MaxLength(5);
            //EventActivityDetailConfig.Maps(v => v.AttachmentContent);
            EventActivityDetailConfig.Maps(v => v.UploadDate).Min(DateTime.Today);
            EventActivityDetailConfig.Maps(v => v.AttachedBy).Min(1);
        }
    }
}
