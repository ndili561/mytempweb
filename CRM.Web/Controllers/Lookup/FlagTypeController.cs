using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class FlagTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public FlagTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private FlagTypeSearchModel InitializeModel(FlagTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "FlagTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(FlagTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetFlagTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetFlagType(id).Result;
            return PartialView(model);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new FlagTypeDto());
        }

        public IActionResult Save(FlagTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutFlagType(model.Id,model).Result
                : _lookupFacadeApiClient.PostFlagType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}