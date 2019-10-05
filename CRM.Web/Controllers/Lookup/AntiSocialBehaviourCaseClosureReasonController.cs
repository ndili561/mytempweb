using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class AntiSocialBehaviourCaseClosureReasonController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AntiSocialBehaviourCaseClosureReasonController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AntiSocialBehaviourCaseClosureReasonSearchModel InitializeModel(AntiSocialBehaviourCaseClosureReasonSearchModel model)
        {
            model = InitializeSearchModel(model, "AntiSocialBehaviourCaseClosureReasonGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(AntiSocialBehaviourCaseClosureReasonSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAntiSocialBehaviourCaseClosureReasons(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var addressType = _lookupFacadeApiClient.GetAntiSocialBehaviourCaseClosureReason(id).Result;
            return PartialView(addressType);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new AntiSocialBehaviourCaseClosureReasonDto());
        }

        public IActionResult Save(AntiSocialBehaviourCaseClosureReasonDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAntiSocialBehaviourCaseClosureReason(model.Id,model).Result
                : _lookupFacadeApiClient.PostAntiSocialBehaviourCaseClosureReason(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}