using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class AlertGroupController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AlertGroupController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AlertGroupSearchModel InitializeModel(AlertGroupSearchModel model)
        {
            model = InitializeSearchModel(model, "AlertGroupGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(AlertGroupSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAlertGroups(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetAlertGroup(id).Result;
            return PartialView(model);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new AlertGroupDto());
        }

        public IActionResult Save(AlertGroupDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAlertGroup(model.Id,model).Result
                : _lookupFacadeApiClient.PostAlertGroup(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}