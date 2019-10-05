using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class AlertStatusController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AlertStatusController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AlertStatusSearchModel InitializeModel(AlertStatusSearchModel model)
        {
            model = InitializeSearchModel(model, "AlertStatusGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(AlertStatusSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAlertStatuses(model).Result;
            return PartialView(result);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new AlertStatusDto());
        }
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetAlertStatus(id).Result;
            return PartialView(model);
        }

        public IActionResult Save(AlertStatusDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAlertStatus(model.Id,model).Result
                : _lookupFacadeApiClient.PostAlertStatus(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}