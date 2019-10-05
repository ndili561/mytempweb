using Core.Utilities.Enum;
using CRM.Entity.Model.Employee;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
    public class RoleController : BaseController
    {
       
        private readonly IRoleApiClient _roleApiClient;
        public RoleController( IRoleApiClient roleApiClient)
        {
            _roleApiClient = roleApiClient;
        }
        public IActionResult Add(int applicationId)
        {
            var model = new RoleDto{ApplicationId = applicationId };
            return PartialView("Edit",model);
        }
        public IActionResult Edit(int id)
        {
            var user = _roleApiClient.GetRole(id).Result;
            return PartialView(user);
        }

        public IActionResult Save(RoleDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _roleApiClient.PutRole(model.Id, model).Result
                : _roleApiClient.PostRole(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage });
        }
      
    }
}