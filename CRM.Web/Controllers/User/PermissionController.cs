using Core.Utilities.Enum;
using CRM.Entity.Model.Employee;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
    public class PermissionController : BaseController
    {
        private readonly IPermissionApiClient _permissionApiClient;
        public PermissionController(IPermissionApiClient permissionApiClient)
        {
            _permissionApiClient = permissionApiClient;
        }
        public IActionResult Add(int roleId)
        {
            var model = new PermissionDto { RoleId = roleId };
            return PartialView("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            var user = _permissionApiClient.GetPermission(id).Result;
            return PartialView(user);
        }

        public IActionResult Save(PermissionDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _permissionApiClient.PutPermission(model.Id, model).Result
                : _permissionApiClient.PostPermission(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage });
        }

    }
}