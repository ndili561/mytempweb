using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class PersonCaseTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public PersonCaseTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private PersonCaseTypeSearchModel InitializeModel(PersonCaseTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "CaseTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(PersonCaseTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetCaseTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var caseType = _lookupFacadeApiClient.GetCaseType(id).Result;
            return PartialView(caseType);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new PersonCaseTypeDto());
        }


        public IActionResult Save(PersonCaseTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutCaseType(model.Id,model).Result
                : _lookupFacadeApiClient.PostCaseType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}