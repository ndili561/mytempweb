using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class AntiSocialBehaviourCaseStatusController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AntiSocialBehaviourCaseStatusController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AntiSocialBehaviourCaseStatusSearchModel InitializeModel(AntiSocialBehaviourCaseStatusSearchModel model)
        {
            model = InitializeSearchModel(model, "AntiSocialBehaviourCaseStatusGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(AntiSocialBehaviourCaseStatusSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAntiSocialBehaviourCaseStatuses(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var addressType = _lookupFacadeApiClient.GetAntiSocialBehaviourCaseStatus(id).Result;
            return PartialView(addressType);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new AntiSocialBehaviourCaseStatusDto());
        }

        public IActionResult Save(AntiSocialBehaviourCaseStatusDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAntiSocialBehaviourCaseStatus(model.Id,model).Result
                : _lookupFacadeApiClient.PostAntiSocialBehaviourCaseStatus(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}