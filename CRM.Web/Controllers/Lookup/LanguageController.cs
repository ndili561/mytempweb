using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class LanguageController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public LanguageController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private LanguageSearchModel InitializeModel(LanguageSearchModel model)
        {
            model = InitializeSearchModel(model, "LanguageGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(LanguageSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetLanguages(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var language = _lookupFacadeApiClient.GetLanguage(id).Result;
            return PartialView(language);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new LanguageDto());
        }
        public IActionResult Save(LanguageDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutLanguage(model.Id ,model).Result
                : _lookupFacadeApiClient.PostLanguage(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}