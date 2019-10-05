using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class TitleController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public TitleController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private TitleSearchModel InitializeModel(TitleSearchModel model)
        {
            model = InitializeSearchModel(model, "TitleGrid", "Name", "Desc");
            return model; 
        }
        public IActionResult Grid(TitleSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetTitles(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var title = _lookupFacadeApiClient.GetTitle(id).Result;
            return PartialView(title);
        }

        public IActionResult Create()
        {
            return PartialView("Edit",new TitleDto());
        }
        public IActionResult Save(TitleDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutTitle(model.Id ,model).Result
                : _lookupFacadeApiClient.PostTitle(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}