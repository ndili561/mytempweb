using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;
using CRM.Web.Helpers;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Customer

{
    public class PersonFlagCommentController : BaseController
    {
        private readonly ICustomerFacadeApiClient _customerFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        public PersonFlagCommentController(ICustomerFacadeApiClient customerFacadeApiClient, ILookupApiClient lookupApiClient)
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
                model = _customerFacadeApiClient.GetPersonFlagComments(model).Result;
                return PartialView("Index", model);
            }
            return View(model);
        }
        private PersonFlagCommentSearchModel InitializeModel(PersonFlagCommentSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonFlagCommentGrid", "RaisedOn", "Desc");
            return model;
        }
        public IActionResult Grid(PersonFlagCommentSearchModel model)
        {
            model = InitializeModel(model);
            var result = _customerFacadeApiClient.GetPersonFlagComments(model).Result;
            return PartialView(result);
        }
        public IActionResult Create(int personFlagId)
        {
            var personFlagComment = new PersonFlagCommentDto { PersonFlagId = personFlagId };
          
            return PartialView("Edit", personFlagComment);
        }
       
        public IActionResult Edit(int id)
        {
            var personFlagComment = _customerFacadeApiClient.GetPersonFlagComment(id).Result;
            return PartialView(personFlagComment);
        }
        [HttpPost]
        public IActionResult Save(PersonFlagCommentDto model)
        {
            bool success;
            model = model.Id == 0 ? _customerFacadeApiClient.PostPersonFlagComment(model).Result
                : _customerFacadeApiClient.PutPersonFlagComment(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}