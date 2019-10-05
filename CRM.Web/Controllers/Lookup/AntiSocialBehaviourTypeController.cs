using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class AntiSocialBehaviourTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AntiSocialBehaviourTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AntiSocialBehaviourTypeSearchModel InitializeModel(AntiSocialBehaviourTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "AntiSocialBehaviourTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(AntiSocialBehaviourTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAntiSocialBehaviourTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var addressType = _lookupFacadeApiClient.GetAntiSocialBehaviourType(id).Result;
            return PartialView(addressType);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new AntiSocialBehaviourTypeDto());
        }

        public IActionResult Save(AntiSocialBehaviourTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAntiSocialBehaviourType(model.Id,model).Result
                : _lookupFacadeApiClient.PostAntiSocialBehaviourType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}