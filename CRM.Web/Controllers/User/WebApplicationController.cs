using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Enum;
using CRM.Entity.Enum;
using CRM.Entity.Helper;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
    public class WebApplicationController : BaseController
    {
        private readonly IEmployeeFacadeApiClient _employeeFacadeApiClient;
        public WebApplicationController(IEmployeeFacadeApiClient employeeFacadeApiClient)
        {
            _employeeFacadeApiClient = employeeFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private WebApplicationSearchModel InitializeModel(WebApplicationSearchModel model)
        {
            if (model == null)
            {
                model = new WebApplicationSearchModel
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
            model.TargetGridId = "WebApplicationGrid";
            return model;
        }
        public IActionResult Grid(WebApplicationSearchModel model)
        {
            model = InitializeModel(model);
            var result = _employeeFacadeApiClient.GetWebApplications(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var webApplication = _employeeFacadeApiClient.GetWebApplication(id).Result;
            return PartialView(webApplication);
        }

        public IActionResult Manage(int id)
        {
            var webApplication = _employeeFacadeApiClient.GetWebApplication(id).Result;
            return View(webApplication);
        }

        public IActionResult Save(WebApplicationDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _employeeFacadeApiClient.PutWebApplication(model.Id, model).Result
                : _employeeFacadeApiClient.PostWebApplication(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage });
        }

        public IActionResult Role(int applicationId)
        {
            var model = InitializeRoleModel(null, applicationId);
            return PartialView(model);
        }
        private RoleSearchModel InitializeRoleModel(RoleSearchModel model, int applicationId)
        {
            if (model == null)
            {
                model = new RoleSearchModel
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
            model.ApplicationId = applicationId;
            model.TargetGridId = "ApplicationRoleGrid";
            return model;
        }
        public async Task<IActionResult> RoleGrid(RoleSearchModel model)
        {
            model = InitializeRoleModel(model, model.ApplicationId);
            var result = await _employeeFacadeApiClient.GetRoles(model);
            return PartialView(result);
        }

        public IActionResult ApplicationUser(int applicationId)
        {
            var model = InitializeUserModel(null, applicationId);
            return PartialView(model);
        }
        private UserSearchModel InitializeUserModel(UserSearchModel model, int applicationId)
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
            model.ApplicationId = applicationId;
            model.TargetGridId = "ApplicationUserGrid";
            return model;
        }
        public async Task<IActionResult> ApplicationUserGrid(UserSearchModel model)
        {
            model = InitializeUserModel(model, model.ApplicationId);
            var result = await _employeeFacadeApiClient.GetUsers(model);
            return PartialView(result);
        }

        public IActionResult MenuItem(int applicationId)
        {
            var model = InitializeMenuItemModel(null, applicationId);
            return PartialView(model);
        }
        private MenuItemSearchModel InitializeMenuItemModel(MenuItemSearchModel model, int applicationId)
        {
            if (model == null)
            {
                model = new MenuItemSearchModel
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
            model.ApplicationId = applicationId;
            model.TargetGridId = "ApplicationMenuItemGrid";
            return model;
        }
        public async Task<IActionResult> MenuItemGrid(MenuItemSearchModel model)
        {
            model = InitializeMenuItemModel(model, model.ApplicationId);
            var result = await _employeeFacadeApiClient.GetMenuItems(model);
            return PartialView(result);
        }
        public IActionResult Permission(int applicationId, int roleId)
        {
            var role = PopulatePermission(applicationId, roleId);
            return PartialView("_RolePermissions", role);
        }

        private RoleDto PopulatePermission(int applicationId, int roleId, List<int> fullaccessIds = null, List<int> readonlyaccessIds = null)
        {
            var searchApplicationRole = new ApplicationRoleSearchModel { ApplicationId = applicationId, RoleId = roleId, PageSize = int.MaxValue, SortColumn = "Id" };
            searchApplicationRole = _employeeFacadeApiClient.GetApplicationRoles(searchApplicationRole).Result;
            var menuItemSearch = new MenuItemSearchModel
            {
                PageSize = int.MaxValue,
                SortColumn = "DisplayName",
                ApplicationId = applicationId
            };
            menuItemSearch = _employeeFacadeApiClient.GetMenuItems(menuItemSearch).Result;
            var fullaccessPermissions = searchApplicationRole.ApplicationRoleSearchResult.SelectMany(a => a.RolePermissions.Where(x => x.AccessLevel == AccessLevel.Full).Select(r => r.MenuItemId)).ToList();
            var readonlyPermissions = searchApplicationRole.ApplicationRoleSearchResult.SelectMany(a => a.RolePermissions.Where(x => x.AccessLevel == AccessLevel.Readonly).Select(r => r.MenuItemId)).ToList();
            var menuItemsForAvailableForReadonly = menuItemSearch.MenuItemSearchResult.Where(x => !fullaccessPermissions.Contains(x.Id)).ToList();
            if (fullaccessIds != null)
            {
                menuItemsForAvailableForReadonly = menuItemsForAvailableForReadonly.Where(x => !fullaccessIds.Contains(x.Id)).ToList();
                fullaccessPermissions.AddRange(fullaccessIds);
            }
            var menuItemsForAvailableForFullaccess = menuItemSearch.MenuItemSearchResult.Where(x => !readonlyPermissions.Contains(x.Id)).ToList();
            if (readonlyaccessIds != null)
            {
                menuItemsForAvailableForFullaccess = menuItemsForAvailableForFullaccess.Where(x => !readonlyaccessIds.Contains(x.Id)).ToList();
                readonlyPermissions.AddRange(readonlyaccessIds);
            }
            var role = _employeeFacadeApiClient.GetRole(roleId).Result;
            role.ApplicationId = applicationId;
            role.Id = roleId;
            role.ReadonlyPermisssionSelectList = SelectedListHelper.GetPermissionSelectList(menuItemsForAvailableForReadonly, readonlyPermissions);
            role.FullAccessPermisssionSelectList = SelectedListHelper.GetPermissionSelectList(menuItemsForAvailableForFullaccess, fullaccessPermissions);
            role.LinkedReadonlyRolePermissionsIds = readonlyaccessIds?.Select(x => x.ToString()).ToList();
            role.LinkedFullAccessRolePermissionsIds = fullaccessIds?.Select(x => x.ToString()).ToList();
            return role;
        }

        public IActionResult RefreshFullAccessRolePermissions(int applicationId, int roleId, string fullaccessIds, string readonlyaccessIds)
        {
            var fullaccessIdInt = GetIntListFromString(fullaccessIds);
            var readonlyaccessIdInt = GetIntListFromString(readonlyaccessIds);
            var role = PopulatePermission(applicationId, roleId, fullaccessIdInt, readonlyaccessIdInt);
            return PartialView("_FullAccessRolePermissions", role);
        }

     

        public IActionResult RefreshReadonlyRolePermissions(int applicationId, int roleId, string fullaccessIds, string readonlyaccessIds)
        {
            var fullaccessIdInt = GetIntListFromString(fullaccessIds);
            var readonlyaccessIdInt = GetIntListFromString(readonlyaccessIds);
            var user = PopulatePermission(applicationId, roleId, fullaccessIdInt, readonlyaccessIdInt);
            return PartialView("_ReadonlyRolePermissions", user);
        }

        public IActionResult SavePermission(RoleDto model)
        {
            int id;
            var message = string.Empty;
            UpdateAuditInformation(model);
            var entity = new ApplicationRoleDto { ApplicationId = model.ApplicationId, RoleId = model.Id };
            if (!ModelState.IsValid)
            {
                message = ModelState.Values.Aggregate(message, (current1, modelState) => modelState.Errors.Aggregate(current1, (current, error) => current + error));
                return Json(new { success = false, message });
            }

            foreach (var fullaccessId in model.LinkedFullAccessRolePermissionsIds)
            {
                int.TryParse(fullaccessId, out id);
                entity.RolePermissions.Add(new PermissionDto { AccessLevel = AccessLevel.Full, MenuItemId = id });
            }
            foreach (var readonlyRoleId in model.LinkedReadonlyRolePermissionsIds)
            {
                int.TryParse(readonlyRoleId, out id);
                entity.RolePermissions.Add(new PermissionDto { AccessLevel = AccessLevel.Readonly, MenuItemId = id });
            }
            model.ApplicationRoles.Add(entity);
            model = _employeeFacadeApiClient.PutRole(model.Id, model).Result;
            message = string.IsNullOrWhiteSpace(model.ErrorMessage) ? model.SuccessMessage : model.ErrorMessage;
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), message });
        }

    }
}