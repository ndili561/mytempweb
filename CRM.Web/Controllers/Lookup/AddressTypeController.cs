using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{

    public class AddressTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public AddressTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private AddressTypeSearchModel InitializeModel(AddressTypeSearchModel model)
        {
            if (model == null)
            {
                model = new AddressTypeSearchModel
                {
                    SortColumn = "Name",
                    SortDirection = "Asc",
                    PageSize = 8,
                    PageNumber = 1
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "Name";
                    model.SortDirection = "Asc";
                }
            }
            model.TargetGridId = "AddressTypeGrid";
            return model;
        }
        public IActionResult Grid(AddressTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetAddressTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var model = _lookupFacadeApiClient.GetAddressType(id).Result;
            return PartialView(model);
        }

        public IActionResult Create()
        {
            return PartialView("Edit", new AddressTypeDto());
        }


        public IActionResult Save(AddressTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutAddressType(model.Id, model).Result
                : _lookupFacadeApiClient.PostAddressType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage });
        }
    }
}