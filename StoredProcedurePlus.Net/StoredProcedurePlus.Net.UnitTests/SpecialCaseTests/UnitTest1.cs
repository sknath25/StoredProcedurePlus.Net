using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTests.SpecialCaseTests
{

    public class sp_user_role_insert_parameters
    {
        public int SpResponseCode { get; set; }
        public string SpResponseMessage { get; set; }
        public string UserSessionToken { get; set; }
        public int? ParentRoleId { get; set; }
        public string RoleName { get; set; }
        public List<sp_user_role_insert_UserSubscribers_parameters> Subscribers { get; set; }
        public List<sp_user_role_insert_ControllerPersmissions_parameters> ControllerActionPermissions { get; set; }

    }
    public class sp_user_role_insert_UserSubscribers_parameters
    {
        public int? user_role_id { get; set; }
        public string user_name { get; set; }
        public int? user_type_id { get; set; }
    }
    public class sp_user_role_insert_ControllerPersmissions_parameters
    {
        public int? user_role_id { get; set; }
        public int? controller_action_id { get; set; }
        public bool is_allowed { get; set; }
    }
    public class sp_user_role_insert : StoredProcedureManager<sp_user_role_insert, sp_user_role_insert_parameters>
    {
        protected override void Setup(ProcedureConfiguration<sp_user_role_insert_parameters> configuration)
        {
            configuration.ConnectionStringName = "OvsDb";
            configuration.Input.Maps(v => v.ParentRoleId);
            configuration.Input.Maps(v => v.RoleName).MaxLength(150).Trim();
            configuration.Input.Maps(v => v.SpResponseCode).Out();
            configuration.Input.Maps(v => v.SpResponseMessage).Out();

            var subscribers = configuration.Input.MapAsTable(v => v.Subscribers, "utt_user_role_subscribers");
            subscribers.Maps(v => v.user_role_id);
            subscribers.Maps(v => v.user_name).MaxLength(250).Trim();
            subscribers.Maps(v => v.user_type_id);

            var controllerPermissions = configuration.Input.MapAsTable(v => v.ControllerActionPermissions, "utt_controller_action_role_permissions");
            controllerPermissions.Maps(v => v.user_role_id);
            controllerPermissions.Maps(v => v.controller_action_id);
            controllerPermissions.Maps(v => v.is_allowed);
        }
    }

    public class sp_user_role_select_result_type_parameters
    {

    }
    public class sp_user_role_select_result_type
    {
        public int? UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string ParentRoleName { get; set; }
        public string UsersForRole { get; set; }
        public string ModulesForRole { get; set; }
    }

    public class sp_user_role_select : StoredProcedureManager<sp_user_role_select, sp_user_role_select_result_type_parameters>
    {
        protected override void Setup(ProcedureConfiguration<sp_user_role_select_result_type_parameters> configuration)
        {
            configuration.ConnectionStringName = "OvsDb";
            var resultUserLogselect = configuration.CanReturnCollectionOf<sp_user_role_select_result_type>();
        }
    }

    [TestClass]
    public class RealWorldSpecialCaseTest
    {

        [TestMethod]
        public void NullRefIssueIssueTest()
        {
            sp_user_role_select_result_type_parameters p = new sp_user_role_select_result_type_parameters();
            sp_user_role_select sp = new sp_user_role_select();

            
            sp.Execute(p);

            sp = new sp_user_role_select();

            sp.Execute(p);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void KeyAlreadyAddedIssueTest()
        {
           
            sp_user_role_insert sp = new sp_user_role_insert();
            sp.Execute(GetParameter(1));
            sp = new sp_user_role_insert();
            sp.Execute(GetParameter(2));
            sp = new sp_user_role_insert();
            sp.Execute(GetParameter(3));

            //var rd = p1.SpResponseCode;
            //var rm = p1.SpResponseMessage;

            Assert.IsTrue(true);
        }

        public sp_user_role_insert_parameters GetParameter(int id)
        {
            sp_user_role_insert_parameters p1 = new sp_user_role_insert_parameters();
            p1.ParentRoleId = null;
            p1.RoleName = id + "-TESTROLE-" + Guid.NewGuid().ToString();
            p1.UserSessionToken = "C8A16C76-2706-402D-B673-6C0EB5D64F6A";
            p1.Subscribers = new List<sp_user_role_insert_UserSubscribers_parameters>
            {
                new sp_user_role_insert_UserSubscribers_parameters() { user_name = id + "-TESTUSER-" + Guid.NewGuid().ToString(), user_role_id = 1, user_type_id = 1 },
                new sp_user_role_insert_UserSubscribers_parameters() { user_name = id + "-TESTUSER-" + Guid.NewGuid().ToString(), user_role_id = 1, user_type_id = 1 },
                new sp_user_role_insert_UserSubscribers_parameters() { user_name = id + "-TESTUSER-" + Guid.NewGuid().ToString(), user_role_id = 1, user_type_id = 1 },
                new sp_user_role_insert_UserSubscribers_parameters() { user_name = id + "-TESTUSER-" + Guid.NewGuid().ToString(), user_role_id = 1, user_type_id = 1 },
                new sp_user_role_insert_UserSubscribers_parameters() { user_name = id + "-TESTUSER-" + Guid.NewGuid().ToString(), user_role_id = 1, user_type_id = 1 }
            };
            p1.ControllerActionPermissions = new List<sp_user_role_insert_ControllerPersmissions_parameters>()
            {
                new sp_user_role_insert_ControllerPersmissions_parameters(){ controller_action_id = 1, user_role_id = 1, is_allowed = true },
                new sp_user_role_insert_ControllerPersmissions_parameters(){ controller_action_id = 2, user_role_id = 1, is_allowed = true },
                new sp_user_role_insert_ControllerPersmissions_parameters(){ controller_action_id = 3, user_role_id = 1, is_allowed = true },
                new sp_user_role_insert_ControllerPersmissions_parameters(){ controller_action_id = 4, user_role_id = 1, is_allowed = false },
            };
            return p1;
        }
    }
}
