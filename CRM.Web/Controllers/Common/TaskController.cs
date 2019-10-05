using System.Linq;
using Core.Utilities.Enum;
using CRM.Entity.Helper;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Common

{
    public class TaskController : BaseController
    {
        private readonly ITaskApiClient _taskApiClient;
        private readonly IWebApplicationApiClient _webApplicationApiClient;
        public TaskController(ITaskApiClient taskApiClient, IWebApplicationApiClient webApplicationApiClient)
        {
            _taskApiClient = taskApiClient;
            _webApplicationApiClient = webApplicationApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private TaskSearchModel InitializeModel(TaskSearchModel model)
        {
            if (model == null)
            {
                model = new TaskSearchModel
                {
                    SortColumn = "Id",
                    SortDirection = "Desc",
                    PageSize = 8,
                    PageNumber = 1
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "Id";
                    model.SortDirection = "Desc";
                }
            }
            model.TargetGridId = "TaskGrid";
            return model;
        }
        public IActionResult Grid(TaskSearchModel model)
        {
            model = InitializeModel(model);
            var result = _taskApiClient.GetTasks(model).Result;
            return PartialView(result);
        }
        public IActionResult Edit(int id)
        {
            var task = _taskApiClient.GetTask(id).Result;
            return PartialView(task);
        }
        public IActionResult Add()
        {
            var model = new TaskDto();
            return PartialView("Edit", model);
        }
        [HttpPost]
        public IActionResult Save(TaskDto model)
        {
            UpdateAuditInformation(model);
            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _taskApiClient.PutTask(model.Id,model).Result
                : _taskApiClient.PostTask(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                return PartialView("Edit", model);
            }
            return Json(new {success=string.IsNullOrWhiteSpace(model.ErrorMessage), message = model.SuccessMessage});
        }
        public IActionResult Details(int taskId)
        {
            var person = GetTaskDetails(taskId);
            return View(person);
        }
        public IActionResult ApplicationTask(int taskId)
        {
            var task = GetTaskDetails(taskId);
            return PartialView("_ApplicationTask", task);
        }
        private TaskDto GetTaskDetails(int taskId)
        {
            var task = _taskApiClient.GetTask(taskId).Result;
            var linkedApplicationIds = task.ApplicationTasks.Select(x => x.ApplicationId).ToList();

            var searchWebApplication = new WebApplicationSearchModel{PageSize = int.MaxValue,SortColumn = "Name",SortDirection = "Asc"};
            searchWebApplication = _webApplicationApiClient.GetWebApplications(searchWebApplication).Result;
            task.ApplicationSelectList = SelectedListHelper.GetApplicationTaskSelectList(searchWebApplication.WebApplicationSearchResult, linkedApplicationIds);
            task.LinkedApplicationTaskIds = linkedApplicationIds.Select(x => x.ToString()).ToList();
            return task;
        }
        
    }
}