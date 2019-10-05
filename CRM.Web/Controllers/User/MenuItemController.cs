using Core.Utilities.Enum;
using CRM.Entity.Model.Employee;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
   
    public class MenuItemController : BaseController
    {
        private readonly IMenuItemApiClient _menuItemApiClient;
        public MenuItemController(IMenuItemApiClient menuItemApiClient)
        {
            _menuItemApiClient = menuItemApiClient;
        }
        public IActionResult Add(int applicationId)
        {
            var model = new MenuItemDto { ApplicationId = applicationId };
            return PartialView("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            var user = _menuItemApiClient.GetMenuItem(id).Result;
            return PartialView(user);
        }

        public IActionResult Save(MenuItemDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _menuItemApiClient.PutMenuItem(model.Id, model).Result
                : _menuItemApiClient.PostMenuItem(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage });
        }

    }
}