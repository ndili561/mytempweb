using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class PriorityController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public PriorityController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private PrioritySearchModel InitializeModel(PrioritySearchModel model)
        {
            model = InitializeSearchModel(model, "PriorityGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(PrioritySearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetPriorities(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetPriority(id).Result;
            return PartialView(model);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new PriorityDto());
        }


        public IActionResult Save(PriorityDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutPriority(model.Id,model).Result
                : _lookupFacadeApiClient.PostPriority(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}