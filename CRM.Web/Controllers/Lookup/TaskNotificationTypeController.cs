using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class TaskNotificationTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public TaskNotificationTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private TaskNotificationTypeSearchModel InitializeModel(TaskNotificationTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "TaskNotificationTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(TaskNotificationTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetTaskNotificationTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var taskNotificationType = _lookupFacadeApiClient.GetTaskNotificationType(id).Result;
            return PartialView(taskNotificationType);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new TaskNotificationTypeDto());
        }
        public IActionResult Save(TaskNotificationTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutTaskNotificationType(model.Id ,model).Result
                : _lookupFacadeApiClient.PostTaskNotificationType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}