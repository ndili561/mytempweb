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
    public class PersonAntiSocialBehaviourCaseNoteController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonAntiSocialBehaviourCaseNoteController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
        {
            _customerFacadeApiClient = customerFacadeApiClient;
            _lookupApiClient = lookupApiClient;
        }
        public IActionResult Index(PersonAntiSocialBehaviourCaseNoteSearchModel model)
        {
             model = InitializeModel(model);
           
            if (Request.IsAjaxRequest())
            {
                model = _customerFacadeApiClient.GetPersonAntiSocialBehaviourCaseNotes(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonAntiSocialBehaviourCaseNoteSearchModel InitializeModel(PersonAntiSocialBehaviourCaseNoteSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonAntiSocialBehaviourCaseNoteGrid", "LoggedDate", "Desc");
            return model;
        }
        public IActionResult Grid(PersonAntiSocialBehaviourCaseNoteSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonAntiSocialBehaviourCaseNotes(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personAntiSocialBehaviourId)
        {
            
            var personAntiSocialBehaviour = _customerFacadeApiClient.GetPersonAntiSocialBehaviour(personAntiSocialBehaviourId).Result;
            var personAntiSocialBehaviourCaseNote = new PersonAntiSocialBehaviourCaseNoteDto
            {
                PersonAntiSocialBehaviourId = personAntiSocialBehaviourId,
                PersonAntiSocialBehaviour = personAntiSocialBehaviour,
                ActionDate = DateTime.Now,
                ActionTimeSelectListItems = SelectedListHelper.GetTimeIntervalForCalendar()
            };
            PopulateLookupFields(personAntiSocialBehaviourCaseNote);
            return PartialView("Edit", personAntiSocialBehaviourCaseNote);
        }

        private void PopulateLookupFields(PersonAntiSocialBehaviourCaseNoteDto personAntiSocialBehaviourDto)
        {
            var lookups = _lookupApiClient.GetLookupUsingOdata(new List<string> { nameof(LookupDto.AntiSocialBehaviourCaseStatuses), nameof(LookupDto.AntiSocialBehaviourCaseClosureReasons), nameof(LookupDto.AntiSocialBehaviourTypes) }).Result;
            var model = lookups.value.FirstOrDefault();
            if (model != null)
            {
                var caseStatuses = model.AntiSocialBehaviourCaseStatuses?.ConvertAll(x => (BaseLookupDto)x);
                var caseTypes = model.AntiSocialBehaviourTypes?.ConvertAll(x => (BaseLookupDto)x);
                var caseClosureReasons = model.AntiSocialBehaviourCaseClosureReasons?.ConvertAll(x => (BaseLookupDto)x);
                personAntiSocialBehaviourDto.PersonAntiSocialBehaviour.CaseStatusSelectListItems =
                    SelectedListHelper.GetSelectListForItems(caseStatuses, personAntiSocialBehaviourDto.PersonAntiSocialBehaviour.CaseStatusId?.ToString());
                personAntiSocialBehaviourDto.PersonAntiSocialBehaviour.CaseTypeSelectListItems =
                    SelectedListHelper.GetSelectListForItems(caseTypes, personAntiSocialBehaviourDto.PersonAntiSocialBehaviour.CaseStatusId?.ToString());
                personAntiSocialBehaviourDto.PersonAntiSocialBehaviour.CaseClosureReasonSelectListItems =
                    SelectedListHelper.GetSelectListForItems(caseClosureReasons, personAntiSocialBehaviourDto.PersonAntiSocialBehaviour.CaseStatusId?.ToString());
            }
            personAntiSocialBehaviourDto.ActionTimeSelectListItems = SelectedListHelper.GetTimeIntervalForCalendar(personAntiSocialBehaviourDto.ActionTime);
            personAntiSocialBehaviourDto.ActionTime = personAntiSocialBehaviourDto.ActionTimeSelectListItems.First(x => x.Selected).Value;

        }

        public IActionResult Edit(int id)
        {
            var personAntiSocialBehaviour = _customerFacadeApiClient.GetPersonAntiSocialBehaviourCaseNote(id).Result;
            PopulateLookupFields(personAntiSocialBehaviour);
            return PartialView(personAntiSocialBehaviour);
        }
        [HttpPost]
        public IActionResult Save(PersonAntiSocialBehaviourCaseNoteDto model)
        {
            bool success;
            var time = model.ActionDate.ToShortDateString().Substring(0, 10) + " " + model.ActionTime + ":00";
            var date = DateTime.Parse(time);
            model.ActionDateTime = date;
            model = model.Id == 0 ? _customerFacadeApiClient.PostPersonAntiSocialBehaviourCaseNote(model).Result
                : _customerFacadeApiClient.PutPersonAntiSocialBehaviourCaseNote(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}