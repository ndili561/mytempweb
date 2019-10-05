using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class FlagGroupController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public FlagGroupController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private FlagGroupSearchModel InitializeModel(FlagGroupSearchModel model)
        {
            model = InitializeSearchModel(model, "FlagGroupGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(FlagGroupSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetFlagGroups(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetFlagGroup(id).Result;
            return PartialView(model);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new FlagGroupDto());
        }


        public IActionResult Save(FlagGroupDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutFlagGroup(model.Id,model).Result
                : _lookupFacadeApiClient.PostFlagGroup(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}