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
    public class PersonFlagController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonFlagController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
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
                model = _customerFacadeApiClient.GetPersonFlags(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonFlagSearchModel InitializeModel(PersonFlagSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonFlagGrid", "RaisedOn", "Desc");
            return model;
        }
        public IActionResult Grid(PersonFlagSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonFlags(model).Result;
            return PartialView(result);
        }

        public IActionResult Create(int personId)
        {
            var person = _customerFacadeApiClient.GetPerson(personId).Result;
            var personFlag = new PersonFlagDto
            {
                PersonId = personId,
                Person = person
            };
            PopulateLookupFields(personFlag);
            return PartialView("Edit",personFlag);
        }
        private void PopulateLookupFields(PersonFlagDto personFlag)
        {
            var lookups = _lookupApiClient.GetLookupUsingOdata(new List<string> { nameof(LookupDto.FlagGroups), nameof(LookupDto.FlagTypes)}).Result;
            var model = lookups.value.FirstOrDefault();
            if (model != null)
            {
                var flagGroups = model.FlagGroups?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                var flagTypes = model.FlagTypes?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
                personFlag.FlagGroupSelectList = SelectedListHelper.GetSelectListForItems(flagGroups, personFlag.FlagGroupId?.ToString());
                personFlag.FlagTypeSelectList = SelectedListHelper.GetSelectListForItems(flagTypes, personFlag.FlagTypeId?.ToString());

            }

        }

        public IActionResult Edit(int id)
        {
            var personFlag = _customerFacadeApiClient.GetPersonFlag(id).Result;
            PopulateLookupFields(personFlag);
            return PartialView(personFlag);
        }

        [HttpPost]
        public IActionResult Save(PersonFlagDto model)
        {
            bool success;
            model = model.Id == 0 ? _customerFacadeApiClient.PostPersonFlag(model).Result
                : _customerFacadeApiClient.PutPersonFlag(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}