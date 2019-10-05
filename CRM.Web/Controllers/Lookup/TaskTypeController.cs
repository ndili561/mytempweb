using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class TaskTypeController : BaseController
    {
        private readonly ITaskTypeApiClient _lookupFacadeApiClient;
        public TaskTypeController(ITaskTypeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private TaskTypeSearchModel InitializeModel(TaskTypeSearchModel model)
        {
            model = InitializeSearchModel(model, "TaskTypeGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(TaskTypeSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetTaskTypes(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var taskReminderType = _lookupFacadeApiClient.GetTaskType(id).Result;
            return PartialView(taskReminderType);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new TaskTypeDto());
        }
        public IActionResult Save(TaskTypeDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutTaskType(model.Id ,model).Result
                : _lookupFacadeApiClient.PostTaskType(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}