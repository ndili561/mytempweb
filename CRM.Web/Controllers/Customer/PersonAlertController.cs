using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Entity.Helper;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Customer;
using CRM.Web.Helpers;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Customer

{
    public class PersonAlertController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonAlertController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
        {
            _customerFacadeApiClient = customerFacadeApiClient;
            _lookupApiClient = lookupApiClient;
        }
        public IActionResult Index(int personId)
        {
            var model = InitializeModel(null);
            model.PersonId = personId;
            if (Request.IsAjaxRequest())
            {
                model = _customerFacadeApiClient.GetPersonAlerts(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonAlertSearchModel InitializeModel(PersonAlertSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonAlertGrid", "RaisedOn", "Desc");
            return model;
        }
        public IActionResult Grid(PersonAlertSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonAlerts(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personId)
        {
            var person = _customerFacadeApiClient.GetPerson(personId).Result;
            var personAlert = new PersonAlertDto { PersonId = personId, Person = person };
            PopulateLookupFields(personAlert);
            return PartialView("Edit", personAlert);
        }
        private void PopulateLookupFields(PersonAlertDto personAlert)
        {
            var model = GetLookupModel();
            if (model != null)
            {
                var alertTypes = model.AlertTypes?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                var alertGroups = model.AlertGroups?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                var alertStatuses = model.AlertStatuses?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                personAlert.AlertTypeSelectList = SelectedListHelper.GetSelectListForItems(alertTypes, personAlert.AlertTypeId?.ToString());
                personAlert.AlertGroupSelectList = SelectedListHelper.GetSelectListForItems(alertGroups, personAlert.AlertGroupId?.ToString());
                personAlert.AlertStatusSelectList = SelectedListHelper.GetSelectListForItems(alertStatuses, personAlert.AlertStatusId?.ToString());
            }
        }

        private LookupDto GetLookupModel()
        {
            var lookups = _lookupApiClient.GetLookupUsingOdata(new List<string>
            {
                nameof(LookupDto.AlertStatuses),
                nameof(LookupDto.AlertGroups),
                nameof(LookupDto.AlertTypes)
            }).Result;
            var model = lookups.value.FirstOrDefault();
            return model;
        }

        public IActionResult Edit(int id)
        {
            var personAlert = _customerFacadeApiClient.GetPersonAlert(id).Result;
            PopulateLookupFields(personAlert);
            return PartialView(personAlert);
        }
        [HttpPost]
        public IActionResult Save(PersonAlertDto model)
        {
            bool success;
            if (model.Id == 0)
            {
                var lookup = GetLookupModel();
                model.AlertStatusId = lookup?.AlertStatuses.FirstOrDefault(x => x.Name == "Active")?.Id;
                model.RaisedOn = DateTime.Now;
                model = _customerFacadeApiClient.PostPersonAlert(model).Result;
            }
            else
            {
                model = _customerFacadeApiClient.PutPersonAlert(model.Id, model).Result;
            }

            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}