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
    public class PersonAntiSocialBehaviourController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonAntiSocialBehaviourController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
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
                model = _customerFacadeApiClient.GetPersonAntiSocialBehaviours(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonAntiSocialBehaviourSearchModel InitializeModel(PersonAntiSocialBehaviourSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonAntiSocialBehaviourGrid", "LoggedDate", "Desc");
            return model;
        }
        public IActionResult Grid(PersonAntiSocialBehaviourSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonAntiSocialBehaviours(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personId)
        {
            
            var person = _customerFacadeApiClient.GetPerson(personId).Result;
            var personAntiSocialBehaviour = new PersonAntiSocialBehaviourDto
            {
                PersonId = personId,
                Person = person,
                ActionDate = DateTime.Now,
                ActionTimeSelectListItems = SelectedListHelper.GetTimeIntervalForCalendar()
            };
            PopulateLookupFields(personAntiSocialBehaviour);
            return PartialView("Edit", personAntiSocialBehaviour);
        }

        private void PopulateLookupFields(PersonAntiSocialBehaviourDto personAntiSocialBehaviourDto)
        {
            var lookups = _lookupApiClient.GetLookupUsingOdata(new List<string> { nameof(LookupDto.AntiSocialBehaviourCaseStatuses), nameof(LookupDto.AntiSocialBehaviourCaseClosureReasons), nameof(LookupDto.AntiSocialBehaviourTypes) }).Result;
            var model = lookups.value.FirstOrDefault();
            if (model != null)
            {
                var caseStatuses = model.AntiSocialBehaviourCaseStatuses?.ConvertAll(x => (BaseLookupDto)x);
                var caseTypes = model.AntiSocialBehaviourTypes?.ConvertAll(x => (BaseLookupDto)x);
                var caseClosureReasons = model.AntiSocialBehaviourCaseClosureReasons?.ConvertAll(x => (BaseLookupDto)x);
                personAntiSocialBehaviourDto.CaseStatusSelectListItems =
                    SelectedListHelper.GetSelectListForItems(caseStatuses, personAntiSocialBehaviourDto.CaseStatusId?.ToString());
                personAntiSocialBehaviourDto.CaseTypeSelectListItems =
                    SelectedListHelper.GetSelectListForItems(caseTypes, personAntiSocialBehaviourDto.CaseStatusId?.ToString());
                personAntiSocialBehaviourDto.CaseClosureReasonSelectListItems =
                    SelectedListHelper.GetSelectListForItems(caseClosureReasons, personAntiSocialBehaviourDto.CaseStatusId?.ToString());
            }
         
           
        }

        public IActionResult Edit(int id)
        {
            var personAntiSocialBehaviour = _customerFacadeApiClient.GetPersonAntiSocialBehaviour(id).Result;
            PopulateLookupFields(personAntiSocialBehaviour);
            return PartialView(personAntiSocialBehaviour);
        }
        [HttpPost]
        public IActionResult Save(PersonAntiSocialBehaviourDto model)
        {
            bool success;
            if (model.Id == 0)
            {
                var time = model.ActionDate.ToShortDateString().Substring(0, 10) + " " + model.ActionTime + ":00";
                var date = DateTime.Parse(time);
                var note = new PersonAntiSocialBehaviourCaseNoteDto
                {
                    Note = model.Notes,
                    ActionDate = date
                };
                model.PersonAntiSocialBehaviourCaseNotes.Add(note);
                model = _customerFacadeApiClient.PostPersonAntiSocialBehaviour(model).Result;
            }
            else
            {
                model = _customerFacadeApiClient.PutPersonAntiSocialBehaviour(model.Id, model).Result;
            }
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
        [HttpGet]
        public JsonResult DeleteDocument(int id)
        {
            if (id > 0)
            {
                var result = _customerFacadeApiClient.DeletePersonAntiSocialBehaviourCaseNote(id).Result;
                var model = result.IsSuccessStatusCode ? "Deleted successfully." : "API error occured.";
                return Json(new { message = model, id, success = result.IsSuccessStatusCode });
            }
            return Json(new { message = "Case note deleted successfully.", id, success = true });
        }
    }
}