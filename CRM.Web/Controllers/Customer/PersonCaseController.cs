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
    public class PersonCaseController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonCaseController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
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
                model = _customerFacadeApiClient.GetPersonCases(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonCaseSearchModel InitializeModel(PersonCaseSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonCaseGrid", "RaisedOn", "Desc");
            return model;
            
        }
        public IActionResult Grid(PersonCaseSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonCases(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personId)
        {
            var person = _customerFacadeApiClient.GetPerson(personId).Result;
            var personCase = new PersonCaseDto { PersonId = personId, Person = person };
            PopulateLookupFields(personCase);
            return PartialView("Edit",personCase);
        }

        public IActionResult Edit(int id)
        {
            var personCase = _customerFacadeApiClient.GetPersonCase(id).Result;
            PopulateLookupFields(personCase);
            return PartialView(personCase);
        }
        private void PopulateLookupFields(PersonCaseDto personCase)
        {
            var lookups = _lookupApiClient.GetLookupUsingOdata(new List<string> { nameof(LookupDto.Priorities), nameof(LookupDto.PersonCaseTypes), nameof(LookupDto.PersonCaseStatuses) }).Result;
            var model = lookups.value.FirstOrDefault();
            if (model != null)
            {
                var caseStatuses = model.PersonCaseStatuses?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                var caseTypes = model.PersonCaseTypes?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                var priorities = model.Priorities?.Where(x=>x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                personCase.CaseStatusSelectList =
                    SelectedListHelper.GetSelectListForItems(caseStatuses, personCase.CaseStatusId?.ToString());
                personCase.CaseTypeSelectList =
                    SelectedListHelper.GetSelectListForItems(caseTypes, personCase.CaseStatusId?.ToString());
                personCase.PrioritySelectList =
                    SelectedListHelper.GetSelectListForItems(priorities, personCase.CaseStatusId?.ToString());

            }

        }

        [HttpPost]
        public IActionResult Save(PersonCaseDto model)
        {
            bool success;
            model = model.Id == 0 ? _customerFacadeApiClient.PostPersonCase(model).Result
                : _customerFacadeApiClient.PutPersonCase(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}