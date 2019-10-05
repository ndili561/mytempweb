using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class EthnicityController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public EthnicityController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private EthnicitySearchModel InitializeModel(EthnicitySearchModel model)
        {
            model = InitializeSearchModel(model, "EthinicityGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(EthnicitySearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetEthnicities(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var ethinicity = _lookupFacadeApiClient.GetEthnicity(id).Result;
            return PartialView(ethinicity);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new EthnicityDto());
        }

        public IActionResult Save(EthnicityDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutEthnicity(model.Id,model).Result
                : _lookupFacadeApiClient.PostEthnicity(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}