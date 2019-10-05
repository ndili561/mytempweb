using CRM.Entity.Model.Common;
using CRM.Entity.Search.Customer;
using CRM.Web.Helpers;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Customer

{
    public class TenantController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        public TenantController(ICustomerFacadeApiClient customerFacadeApiClient)
        {
            _customerFacadeApiClient = customerFacadeApiClient;
        }
        public IActionResult Index(string propertyCode)
        {
            var model = InitializeModel(null);
            model.PropertyCode = propertyCode;
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return View(model);
        }
        public IActionResult Details(string tenantCode)
        {
            var model = InitializeModel(null);
            model.TenantCode = tenantCode;
            if (Request.IsAjaxRequest())
            {
                model = _customerFacadeApiClient.GetTenants(model).Result;
                return PartialView("Index", model);
            }
            return View("Index", model);
        }
        private TenantSearchModel InitializeModel(TenantSearchModel model)
        {
            if (model == null)
            {
                model = new TenantSearchModel
                {
                    SortColumn = "TenantCode",
                    SortDirection = "Asc",
                    PageSize = 8,
                    PageNumber = 1
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "TenantCode";
                    model.SortDirection = "Asc";
                }
            }
            model.TargetGridId = "TenantGrid";
            return model;
        }
        public IActionResult Grid(TenantSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetTenants(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personId)
        {
            var person = _customerFacadeApiClient.GetPerson(personId).Result;
            var personAlert = new TenantDto { PersonId = personId, Person = person };
            return PartialView("Edit", personAlert);
        }

        public IActionResult Edit(int id)
        {
            var caseType = _customerFacadeApiClient.GetTenant(id).Result;
            return PartialView(caseType);
        }
        [HttpPost]
        public IActionResult Save(TenantDto model)
        {
            bool success;
            model = model.Id == 0 ? _customerFacadeApiClient.PostTenant(model).Result
                : _customerFacadeApiClient.PutTenant(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}