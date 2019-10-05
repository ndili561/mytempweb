using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class RelationshipController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public RelationshipController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private RelationshipSearchModel InitializeModel(RelationshipSearchModel model)
        {
            model = InitializeSearchModel(model, "RelationshipGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(RelationshipSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetRelationships(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var language = _lookupFacadeApiClient.GetRelationship(id).Result;
            return PartialView(language);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new RelationshipDto());
        }

        public IActionResult Save(RelationshipDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutRelationship(model.Id ,model).Result
                : _lookupFacadeApiClient.PostRelationship(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}