using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class NationalityTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public NationalityTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private NationalityTypeSearchModel InitializeModel(NationalityTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "NationalityTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(NationalityTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetNationalityTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var nationalityType = _lookupFacadeApiClient.GetNationalityType(id).Result;
            return PartialView(nationalityType);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new NationalityTypeDto());
        }
        public IActionResult Save(NationalityTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutNationalityType(model.Id ,model).Result
                : _lookupFacadeApiClient.PostNationalityType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}