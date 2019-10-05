using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class TaskReminderTypeController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public TaskReminderTypeController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private TaskReminderTypeSearchModel InitializeModel(TaskReminderTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "TaskReminderTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(TaskReminderTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetTaskReminderTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var taskReminderType = _lookupFacadeApiClient.GetTaskReminderType(id).Result;
            return PartialView(taskReminderType);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new TaskReminderTypeDto());
        }

        public IActionResult Save(TaskReminderTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutTaskReminderType(model.Id ,model).Result
                : _lookupFacadeApiClient.PostTaskReminderType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}