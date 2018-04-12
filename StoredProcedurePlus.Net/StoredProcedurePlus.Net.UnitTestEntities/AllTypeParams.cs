using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTestEntities
{
    public class AllTypeParams
    {
        public long Id { get; set; } 
        public bool IsEnabled { get; set; }
        public int? RowChanged { get; set; }
        public List<AllTypeChildParams> Childs { get; set; }
    }

    public class AllTypeChildParams
    {
        public long Id { get; set; }
        public bool IsEnabled { get; set; }
        public int? RowChanged { get; set; }
    }

    public class University
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int UniversityType { get; set; }
        public List<SchoolType> Schools { get; set; }
    }

    public class SchoolType
    {
        public int UniversityId { get; set; }
        public int SchoolId { get; set; }
        public short SType { get; set; }        
        public string SchoolName { get; set; }
        public string SchoolDescription { get; set; }
        public string SchoolAddress { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public int SchoolId { get; set; }
        public short StudentType { get; set; }        
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public string StudentAddress { get; set; }
    }

 
    public class SP_Insert_Event_Workflow_ActivityTransactionParamters
    {
        /*
            @EventName  varchar(200)
           ,@EventDescription  nvarchar(500)
           ,@HasVoting bit
		   ,@HasAttachment bit
		   ,@HasWorkflow bit
		   ,@WorkflowId int
           ,@CategoryId int
           ,@LocationId int = null
           ,@DepartmentId int = null
           ,@SectionId int = null
           ,@VotingStartDate datetime
           ,@VotingEndDate datetime
           ,@RefEventId int = NULL
           ,@CreatedBy int
		   ,@EventActivityDetail EventActivityInsertTable Readonly
		   ,@OutStatusID int OUT 
        */
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public bool HasVoting { get; set; }
        public bool HasAttachment { get; set; }
        public bool HasWorkflow { get; set; }
        public int WorkflowId { get; set; }
        public int CategoryId { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public DateTime? VotingStartDate { get; set; }
        public DateTime? VotingEndDate { get; set; }
        public int? RefEventId { get; set; }
        public int CreatedBy { get; set; }
        public List<EventActivityInsertTable> EventActivityDetail { get; set; }
        public int OutStatusID { get; set; }
    }

    public class EventActivityInsertTable
    {
        public int? MasterWorkflowId { get; set; }
        public int? EventId { get; set; }
        public int? ActivityId { get; set; }
        public string ModifiedDescription { get; set; }
        public int? CustomSequence { get; set; }
        public int? AssignedToGroupId { get; set; }
        public string AssignedToEmailId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public int? AssignmentTypeId { get; set; }
        public int? CreatedBy { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentExtension { get; set; }
        public byte[] AttachmentContent { get; set; }
        public DateTime? UploadDate { get; set; }
        public int? AttachedBy { get; set; }
    }

 
}
