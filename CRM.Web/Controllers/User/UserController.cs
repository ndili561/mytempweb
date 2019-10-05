using System.Linq;
using Core.Utilities.Enum;
using CRM.Entity.Helper;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
    public class UserController : BaseController
    {
        private readonly IEmployeeFacadeApiClient _employeeApiClient;
        public UserController(IEmployeeFacadeApiClient employeeApiClient)
        {
            _employeeApiClient = employeeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private UserSearchModel InitializeModel(UserSearchModel model)
        {
            if (model == null)
            {
                model = new UserSearchModel
                {
                    SortColumn = "Id",
                    SortDirection = "Desc",
                    PageSize = 8,
                    PageNumber = 1
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "Id";
                    model.SortDirection = "Desc";
                }
            }
            model.TargetGridId = "UserGrid";
            return model;
        }
        public IActionResult Grid(UserSearchModel model)
        {
            model = InitializeModel(model);
            var result = _employeeApiClient.GetUsers(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int userId)
        {
            var user = _employeeApiClient.GetUser(userId).Result;
            return PartialView(user);
        }

        public IActionResult Save(UserDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _employeeApiClient.PutUser(model.Id, model).Result
                : _employeeApiClient.PostUser(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new {success=string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage });
        }

        public IActionResult SaveUserApplications(UserDto model)
        {
            UpdateAuditInformation(model);
            if (model.LinkedUserApplicationIds != null)
            {
                foreach (var applicationId in model.LinkedUserApplicationIds)
                {
                    var appId = 0;
                    int.TryParse(applicationId, out appId);
                    if (appId > 0)
                    {
                        model.Applications.Add(new ApplicationUserDto {UserId = model.Id, ApplicationId = appId});
                    }
                }
            }
            else
            {
                model.Applications.Add(new ApplicationUserDto { UserId = model.Id, ApplicationId = 0 });
            }
            model = _employeeApiClient.PutUser(model.Id, model).Result;
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage });
        }

        public IActionResult SaveUserApplicationRoles(UserDto model)
        {
            UpdateAuditInformation(model);
            if (model.LinkedUserApplicationRoleIds!=null)
            {
                foreach (var applicationRoleId in model.LinkedUserApplicationRoleIds)
                {
                    var roleId = 0;
                    int.TryParse(applicationRoleId, out roleId);
                    if (roleId > 0)
                    {
                        model.Roles.Add(new UserApplicationRoleDto { UserId = model.Id, ApplicationRoleId = roleId });
                    }
                }
            }
            else
            {
                model.Roles.Add(new UserApplicationRoleDto { UserId = model.Id, ApplicationRoleId = 0 });
            }
            model = _employeeApiClient.PutUser(model.Id, model).Result;
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage });
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Details(int userId)
        {
            var person = GetUserForDetails(userId,true);
            return View(person);
        }
        public IActionResult UserApplications(int userId)
        {
            var user = GetUserForDetails(userId,false);
            return PartialView("_UserApplications", user);
        }
        public IActionResult UserRoles(int userId)
        {
            var user = GetUserForDetails(userId,false);
            user.ApplicationSelectList = user.ApplicationSelectList.Where(x => x.Selected).ToList();
            int applicationId;
            int.TryParse(user.LinkedUserApplicationIds.FirstOrDefault(), out applicationId);
            if (applicationId > 0)
            {
                user = PopulateRolesForUser(user, applicationId, userId);
            }
            return PartialView("_UserRoles", user);
        }

        public IActionResult GetApplicationRoles(int applicationId,int userId)
        {
            var user = new UserDto{Id= userId };
            user = PopulateRolesForUser(user, applicationId, userId);
            return PartialView("_ApplicationRoles", user);
        }
        private UserDto GetUserForDetails(int userId,bool loadManager)
        {
            var userSearchModel = new UserSearchModel { PageSize = int.MaxValue, SortColumn = "Name" };
            var user = _employeeApiClient.GetUser(userId).Result;
            if (loadManager)
            {
                var managers = _employeeApiClient.GetUsers(userSearchModel).Result.UserSearchResult.Where(x => x.Id != userId).ToList();
                user.ManagerSelectList = SelectedListHelper.GetSelectListForManager(managers, user.ManagerId?.ToString());
            }
            var linkedUserApplicationIds = user.Applications.Select(x => x.ApplicationId).ToList();
            var userGroupSearch = new UserGroupSearchModel { PageSize = int.MaxValue, SortColumn = "Name" };
            userGroupSearch = _employeeApiClient.GetUserGroups(userGroupSearch).Result;
            user.UserGroupSelectList = SelectedListHelper.GetSelectListForUserGroup(userGroupSearch.UserGroupSearchResult, user.UserGroupId?.ToString());
         
            var searchWebApplication = new WebApplicationSearchModel { PageSize = int.MaxValue, SortColumn = "Name", SortDirection = "Asc" };
            searchWebApplication = _employeeApiClient.GetWebApplications(searchWebApplication).Result;
            user.ApplicationSelectList = SelectedListHelper.GetApplicationTaskSelectList(searchWebApplication.WebApplicationSearchResult, linkedUserApplicationIds);
            user.LinkedUserApplicationIds = linkedUserApplicationIds.Select(x => x.ToString()).ToList();
            return user;
        }

        private UserDto PopulateRolesForUser(UserDto model, int applicationId,int userId)
        {
            var searchUserApplicationRoles = new UserApplicationRoleSearchModel { PageSize = int.MaxValue, SortColumn = "Role/Role/RoleName", SortDirection = "Asc", ApplicationId = applicationId,UserId = userId};
            searchUserApplicationRoles = _employeeApiClient.GetUserApplicationRoles(searchUserApplicationRoles).Result;
            var searchRoles = new RoleSearchModel { PageSize = int.MaxValue, SortColumn = "RoleName", SortDirection = "Asc", ApplicationId = applicationId };
            searchRoles = _employeeApiClient.GetRoles(searchRoles).Result;
            model.RoleSelectList = SelectedListHelper.GetApplicationRolesSelectList(searchRoles.RoleSearchResult, searchUserApplicationRoles.UserApplicationRoleSearchResult.Select(x=>x.Role.Id).ToList());
            return model;
        }
    }
}