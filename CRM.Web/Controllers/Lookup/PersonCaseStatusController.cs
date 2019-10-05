using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class PersonCaseStatusController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public PersonCaseStatusController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private PersonCaseStatusSearchModel InitializeModel(PersonCaseStatusSearchModel model)
        {
            model = InitializeSearchModel(model, "CaseStatusGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(PersonCaseStatusSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetCaseStatuses(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var CaseStatus = _lookupFacadeApiClient.GetCaseStatus(id).Result;
            return PartialView(CaseStatus);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new PersonCaseStatusDto());
        }


        public IActionResult Save(PersonCaseStatusDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutCaseStatus(model.Id,model).Result
                : _lookupFacadeApiClient.PostCaseStatus(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}