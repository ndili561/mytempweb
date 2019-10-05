using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class AlertTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AlertTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AlertTypeSearchModel InitializeModel(AlertTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "AlertTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(AlertTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAlertTypes(model).Result;
            return PartialView(result);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetAlertType(id).Result;
            return PartialView(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Edit" ,new AlertTypeDto());
        }

        public IActionResult Save(AlertTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAlertType(model.Id,model).Result
                : _lookupFacadeApiClient.PostAlertType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}