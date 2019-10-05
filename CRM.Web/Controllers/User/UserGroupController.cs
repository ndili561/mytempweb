using Core.Utilities.Enum;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
    public class UserGroupController : BaseController
    {
        private readonly IEmployeeFacadeApiClient _employeeFacadeApiClient;
        public UserGroupController(IEmployeeFacadeApiClient employeeFacadeApiClient)
        {
            _employeeFacadeApiClient = employeeFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private UserGroupSearchModel InitializeModel(UserGroupSearchModel model)
        {
            if (model == null)
            {
                model = new UserGroupSearchModel
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
            model.TargetGridId = "UserGroupGrid";
            return model;
        }
        public IActionResult Grid(UserGroupSearchModel model)
        {
            model = InitializeModel(model);
            var result = _employeeFacadeApiClient.GetUserGroups(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int userGroupId)
        {
            var userGroup = _employeeFacadeApiClient.GetUserGroup(userGroupId).Result;
            return PartialView(userGroup);
        }
        public IActionResult Add()
        {
            return PartialView("Edit", new UserGroupDto());
        }
        public IActionResult Save(UserGroupDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _employeeFacadeApiClient.PutUserGroup(model.Id ,model).Result
                : _employeeFacadeApiClient.PostUserGroup(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}