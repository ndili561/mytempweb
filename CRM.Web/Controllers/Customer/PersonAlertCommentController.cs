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
    public class PersonAlertCommentController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonAlertCommentController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
        {
            _customerFacadeApiClient = customerFacadeApiClient;
            _lookupApiClient = lookupApiClient;
        }
        public IActionResult Index(int personAlertId)
        {
            var model = InitializeModel(null);
            model.PersonAlertId = personAlertId;
            if (Request.IsAjaxRequest())
            {
                model = _customerFacadeApiClient.GetPersonAlertComments(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonAlertCommentSearchModel InitializeModel(PersonAlertCommentSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonAlertCommentGrid", "RaisedOn", "Desc");
            return model;
        }
        public IActionResult Grid(PersonAlertCommentSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonAlertComments(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personAlertId)
        {
            var personAlert = _customerFacadeApiClient.GetPersonAlert(personAlertId).Result;
            PopulateLookupFields(personAlert);
            var personAlertComment = new PersonAlertCommentDto { PersonAlertId = personAlertId,PersonAlert = personAlert };
            return PartialView("Edit", personAlertComment);
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
            var personAlertComment = _customerFacadeApiClient.GetPersonAlertComment(id).Result;
            return PartialView(personAlertComment);
        }

        [HttpPost]
        public IActionResult Save(PersonAlertCommentDto model)
        {
            bool success;
            model = model.Id == 0 ? _customerFacadeApiClient.PostPersonAlertComment(model).Result
                : _customerFacadeApiClient.PutPersonAlertComment(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}