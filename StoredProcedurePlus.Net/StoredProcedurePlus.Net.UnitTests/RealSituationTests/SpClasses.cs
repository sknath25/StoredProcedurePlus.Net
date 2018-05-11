using StoredProcedurePlus.Net.StoredProcedureManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTests.RealSituationTests
{
    public class SP_FetchAssignmentTypeResultType
    {
        public int AssignmentTypeId { get; set; }
        public string AssignmentType { get; set; }
    }
    public class SP_FetchAssignmentType : StoredProcedureManager<
        SP_FetchAssignmentType, Object>
    {
        protected override void Setup(ProcedureConfiguration<Object> configuration)
        {
            configuration.ConnectionStringName = "OvsDb";
            var resultconfig = configuration.CanReturnCollectionOf<SP_FetchAssignmentTypeResultType>();
            resultconfig.Maps(v => v.AssignmentType).Trim();
        }
    }

    public class SP_SuggestedWorkflowResultType
    {
        public int WorkflowId { get; set; }
        public string WorkFlowName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class SP_SuggestedWorkflow : StoredProcedureManager<
        SP_SuggestedWorkflow, Object>
    {
        protected override void Setup(ProcedureConfiguration<object> configuration)
        {
            configuration.ConnectionStringName = "OvsDb";
            configuration.Input.IncludeUnmappedProperties = false;
            var resultconfig = configuration.CanReturnCollectionOf<SP_SuggestedWorkflowResultType>();
            //resultconfig.Maps(v => v.WorkFlowName).Trim();
            //resultconfig.Maps(v => v.Description).Trim();
            //resultconfig.Maps(v => v.CategoryName).Trim();
        }
    }


    public class sp_campaign_select_paramters
    {
        public int? PageNo { get; set; }
        public int? RecordPerPage { get; set; }
        public string CampaignNameContains { get; set; }
    }

    public class sp_campaign_select_result_type
    {
        public long RecordIndex { get; set; }
        public int? ParentCampaignId { get; set; }
        public string ParentCampaignName { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDesc { get; set; }
        public string CampaignStatus { get; set; }
        public int TotalEvents { get; set; }
        public DateTime? EventsStartDate { get; set; }
        public DateTime? EventsEndDate { get; set; }
    }

    public class sp_campaign_select : StoredProcedureManager<sp_campaign_select, sp_campaign_select_paramters>
    {
        protected override void Setup(ProcedureConfiguration<sp_campaign_select_paramters> configuration)
        {
            configuration.ConnectionStringName = "OvsDb";
            configuration.Input.Maps(v => v.CampaignNameContains).MaxLength(50).Trim();
            var resultcampaignselect = configuration.CanReturnCollectionOf<sp_campaign_select_result_type>();
        }
    }
}
