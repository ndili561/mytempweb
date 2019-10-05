using Core.Utilities.Enum;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Lookup

{
   
    public class TaskStatusController : BaseController
    {
        private readonly ILookupFacadeApiClient _lookupFacadeApiClient;
        public TaskStatusController(ILookupFacadeApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private TaskStatusSearchModel InitializeModel(TaskStatusSearchModel model)
        {
            model = InitializeSearchModel(model, "TaskStatusGrid", "Name", "Desc");
            return model;
        }
        public IActionResult Grid(TaskStatusSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetTaskStatuses(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int taskStatusId)
        {
            var taskStatus = _lookupFacadeApiClient.GetTaskStatus(taskStatusId).Result;
            return PartialView(taskStatus);
        }
        public IActionResult Create()
        {
            return PartialView("Edit", new TaskStatusDto());
        }


        public IActionResult Save(TaskStatusDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _lookupFacadeApiClient.PutTaskStatus(model.Id ,model).Result
                : _lookupFacadeApiClient.PostTaskStatus(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new { message = model.SuccessMessage});
        }
    }
}