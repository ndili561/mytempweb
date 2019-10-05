using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class GenderController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public GenderController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private GenderSearchModel InitializeModel(GenderSearchModel model)
        {
            model = InitializeSearchModel(model, "GenderGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(GenderSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetGenders(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var gender = _lookupFacadeApiClient.GetGender(id).Result;
            return PartialView(gender);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new GenderDto());
        }
        public IActionResult Save(GenderDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutGender(model.Id,model).Result
                : _lookupFacadeApiClient.PostGender(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}