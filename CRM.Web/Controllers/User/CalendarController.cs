using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity.Helper;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Common;
using CRM.Entity.Search.Employee;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Web.Controllers.User

{
   
    public class CalendarController : BaseController
    {
        private readonly IEmployeeFacadeApiClient _employeeFacadeApiClient;
        private readonly ITaskTypeApiClient _taskTypeApiClient;
        private readonly IPersonApiClient _personApiClient;
        public CalendarController(IEmployeeFacadeApiClient employeeFacadeApiClient, ITaskTypeApiClient taskTypeApiClient, IPersonApiClient personApiClient)
        {
            _employeeFacadeApiClient = employeeFacadeApiClient;
            _taskTypeApiClient = taskTypeApiClient;
            _personApiClient = personApiClient;
        }
        public IActionResult Index()
        {
            var calendarTask = GetCalendarTaskModel();
            return View(calendarTask);
        }
        public IActionResult Schedule()
        {
            var calendarTask = GetCalendarTaskModel();
            return View(calendarTask);
        }
        public IActionResult GetCalendarTask(int employeeId)
        {
            var calendarTask = GetTaskModel(employeeId);
            return PartialView("_CalendarTask", calendarTask);
        }
        public IActionResult GetSchedule(string employeeIds, int taskTypeId)
        {
            var ids = GetIntListFromString(employeeIds);
            var model = GetTaskSchedule(ids, taskTypeId);
            return PartialView("_ScheduleTask", model);
        }
        private List<JsonEmployeeTask> GetTaskModel(int employeeId)
        {
            var dow = new[] { 1, 2, 3, 4, 5 };// Monday - Friday
            var employeeCalendarSearchmodel = new UserTaskSearchModel { UserId = employeeId, PageSize = int.MaxValue, SortColumn = "Id" };
            employeeCalendarSearchmodel = _employeeFacadeApiClient.GetUserTasks(employeeCalendarSearchmodel).Result;
            var result = (from calendarTask in employeeCalendarSearchmodel.UserTaskSearchResult
                          select new JsonEmployeeTask
                          {
                              id = calendarTask.Id,
                              title = calendarTask.TaskType?.Name,
                              description = calendarTask.Description,
                              start = calendarTask.ScheduleStartTime.ToString("s"),
                              end = calendarTask.ScheduleEndTime.ToString("s"),
                              className = calendarTask.TaskType?.CssClass.ToLower(),
                              styleName = calendarTask.TaskType?.CssStyle.ToLower(),
                              businessHours = new JsonBusinessHours { dow = dow, start = "08:00", end = "17:00" }
                          }).ToList();
            return result;
        }
        private CalendarSearchModel GetCalendarTaskModel(UserPersonTaskDto userPersonTask = null)
        {
            var model = new UserSearchModel { PageSize = int.MaxValue};
            model = _employeeFacadeApiClient.GetUsers(model).Result;
            //var personSearch = new PersonSearchModel { PageSize = int.MaxValue };
            //personSearch = _personApiClient.GetPersons(personSearch).Result;
            var taskTypeSearchmodel = new TaskTypeSearchModel { PageSize = int.MaxValue};
            taskTypeSearchmodel = _taskTypeApiClient.GetTaskTypes(taskTypeSearchmodel).Result;
            var selectedTaskTypeId = taskTypeSearchmodel.TaskTypeSearchResult.FirstOrDefault(x => x.Name == userPersonTask?.TaskTypeSelected)?.Id;
            var taskTypes = taskTypeSearchmodel.TaskTypeSearchResult?.ConvertAll(x => (BaseLookupDto)x);

            var applications = new WebApplicationSearchModel { PageSize = int.MaxValue};
            applications = _employeeFacadeApiClient.GetWebApplications(applications).Result;



            var userCalendarModel = new CalendarSearchModel
            {
                ApplicationList = SelectedListHelper.GetApplicationSelectList(applications.WebApplicationSearchResult, userPersonTask?.ApplicationId.ToString()),
                EmployeeList = SelectedListHelper.GetUserSelectList(model.UserSearchResult, userPersonTask?.UserId.ToString()),
                TaskTypes = taskTypeSearchmodel.TaskTypeSearchResult,
                TaskTypeList = SelectedListHelper.GetSelectListForItems(taskTypes, selectedTaskTypeId?.ToString()),
                TaskTypeId = selectedTaskTypeId,
                //PersonList = SelectedListHelper.GetPersonSelectList(personSearch.PersonSearchResult, userPersonTask?.PersonId.ToString())
            };
            return userCalendarModel;
        }
        private JsonResult GetTaskSchedule(List<int> userIds, int taskId)
        {
            var resourceList = new List<JsonEmployeeTask>();
            var taskList = new List<JsonEmployeeTask>();
            foreach (var userId in userIds)
            {
                var userSearchmodel = new UserSearchModel { Id = userId, TaskId = taskId, PageSize = 10, SortColumn = "Id" };
                userSearchmodel = _employeeFacadeApiClient.GetUsersWithTasks(userSearchmodel).Result;
                var tasks = userSearchmodel.UserSearchResult.SelectMany(x => x.Tasks).ToList();
                var resources = (from contact in userSearchmodel.UserSearchResult
                                 select new JsonEmployeeTask
                                 {
                                     id = contact.Id,
                                     title = contact.Name,
                                     className = contact.Tasks.FirstOrDefault()?.TaskTypeId != 5 ? contact.Tasks.FirstOrDefault()?.TaskType.CssClass : contact.Tasks.FirstOrDefault()?.Task?.TaskCss?.ToLower()
                                 }).ToList();
                resourceList.AddRange(resources);
                var userTasks = tasks.Select(task => new JsonEmployeeTask
                {
                    id = task.Id,
                    resourceId = task.UserId,
                    start = task.ScheduleStartTime.ToString("s"),
                    end = task.ScheduleEndTime.ToString("s"),
                    title = task.TaskTypeId != 5 ? task.TaskType.Name : task.Description,
                    name = task.TaskTypeId != 5 ? task.TaskType.Name : task.Task.Name,
                    styleName = task.TaskTypeId != 5 ? task.TaskType.CssStyle : task.Task?.TaskStyle,
                    className = task.TaskTypeId != 5 ? task.TaskType.CssClass : task.Task?.TaskCss.ToString().ToLower()
                })
                    .ToList();

                taskList.AddRange(userTasks);
            }
            return Json(new { contacts = resourceList, tasks = taskList });
        }


        public ActionResult Create(UserPersonTaskDto model)
        {
            var calendarModel = GetCalendarTaskModel(model);
            model.ApplicationList = calendarModel.ApplicationList;
            model.TaskTypeList = calendarModel.TaskTypeList;
            model.ApplicationList = calendarModel.ApplicationList;
            model.PersonList = calendarModel.PersonList;
            model.EmployeeList = calendarModel.EmployeeList;
            model.ScheduleStartDate = model.ScheduleStartTime.Date;
            model.ScheduleEndDate = model.ScheduleEndTime.Date;
            model.ScheduleStartTimeString = model.ScheduleStartTime.ToShortTimeString();
            model.ScheduleEndTimeString = model.ScheduleEndTime.ToShortTimeString();
            return PartialView("_Create", model);
        }
        [HttpPost]
        public ActionResult CreateUserTask(UserPersonTaskDto model)
        {
            UpdateAuditInformation(model);
            DateTime scheduleStartTime = Convert.ToDateTime(model.ScheduleStartTimeString);
            DateTime scheduleEndTime = Convert.ToDateTime(model.ScheduleEndTimeString);
            model.ScheduleStartTime = model.ScheduleStartDate.AddHours(scheduleStartTime.Hour).AddMinutes(scheduleStartTime.Minute);
            model.ScheduleEndTime = model.ScheduleStartDate.AddHours(scheduleEndTime.Hour).AddMinutes(scheduleEndTime.Minute);
            var result = model.Id > 0 ? _employeeFacadeApiClient.PutUserTask(model.Id, model).Result :
                _employeeFacadeApiClient.PostUserTask(model).Result;

            return Json(new { message = "Task added to calendar successfully.", model.UserId, success = true });
        }
        public ActionResult Edit(int id)
        {
            var model = _employeeFacadeApiClient.GetUserTask(id).Result;
            var calendarModel = GetCalendarTaskModel(model);
            model.ApplicationList = calendarModel.ApplicationList;
            model.TaskTypeList = calendarModel.TaskTypeList;
            model.ApplicationList = calendarModel.ApplicationList;
            model.PersonList = calendarModel.PersonList;
            model.EmployeeList = calendarModel.EmployeeList;
            model.ScheduleStartDate = model.ScheduleStartTime.Date;
            model.ScheduleEndDate = model.ScheduleEndTime.Date;
            model.ScheduleStartTimeString = model.ScheduleStartTime.ToShortTimeString();
            model.ScheduleEndTimeString = model.ScheduleEndTime.ToShortTimeString();
            return PartialView("_Create", model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(UserDto model)
        {
            if (model.UploadedFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.UploadedFile.CopyTo(ms);
                    model.FileContent = ms.ToArray();
                    model = await _employeeFacadeApiClient.PutUserTaskCalendarFileUpload(model.Id, model);
                    return Json(string.IsNullOrWhiteSpace(model.ErrorMessage)? model.SuccessMessage: model.ErrorMessage);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Download(int employeeId)
        {
            if (employeeId == 0)
                return Content("Please select Employee.");
            var document = await _employeeFacadeApiClient.GetUserTaskCalendarFile(employeeId);
            Stream stream = new MemoryStream(document.FileContent);
            document.Name += ".ics";
            return File(stream, document.Name.GetContentType(), document.Name);
        }
        [HttpPost]
        public JsonResult RescheduleCalendarTask(int userPersonTaskId, int employeeId, DateTime scheduleStartDate, DateTime scheduleEndDate)
        {
            var model = new UserPersonTaskDto { Id = userPersonTaskId, UserId = employeeId, ScheduleStartDate = scheduleStartDate };
            model.ScheduleStartDate = model.ScheduleStartTime.Date;
            model.ScheduleEndDate = model.ScheduleEndTime.Date;
            model.ScheduleStartTime = scheduleEndDate;
            model.ScheduleEndTimeString = model.ScheduleEndTime.ToShortTimeString();
            UpdateAuditInformation(model);
            var result = _employeeFacadeApiClient.PutUserTask(model.Id, model).Result;
            return Json(new { success = string.IsNullOrWhiteSpace(result.ErrorMessage), message = string.IsNullOrWhiteSpace(result.ErrorMessage)?result.SuccessMessage: result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult UpdateCalender([FromBody]CalendarTask model)
        {
            int employeeId = model.EmployeeId;
            string calendarViewType = model.CalendarViewType;
            string calendarStartDate = model.CalendarStartDate;
            string calendarEndDate = model.CalendarEndDate;
            var calendarTasks = JsonConvert.DeserializeObject<IEnumerable<JsonEmployeeTask>>(model.Events);
            var user = new UserDto
            {
                Id = employeeId,
                CalendarViewType = calendarViewType
            };
            var dtCalendarDate = DateTime.Parse(calendarStartDate, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            user.CalendarStartDate = dtCalendarDate;
            dtCalendarDate = DateTime.Parse(calendarEndDate, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            user.CalendarEndDate = dtCalendarDate;
            foreach (var calendarTask in calendarTasks)
            {
                DateTime.TryParse(calendarTask.start, out DateTime startDate);
                DateTime.TryParse(calendarTask.end, out DateTime endDate);
                var task = new UserPersonTaskDto
                {
                    Id = (int)calendarTask.id,
                    Description = calendarTask.id == 0 ? calendarTask.title : calendarTask.description,
                    TaskName = calendarTask.title,
                    UserId = employeeId,
                    ScheduleStartTime = startDate,
                    ScheduleEndTime = endDate == DateTime.MinValue ? startDate.AddMinutes(30) : endDate,
                };
                UpdateAuditInformation(task);
                user.Tasks.Add(task);
            }
            var result = _employeeFacadeApiClient.PatchUserTasks(user.Id, user).Result;
            var message = string.IsNullOrWhiteSpace(result.ErrorMessage) ? result.SuccessMessage : result.ErrorMessage;
            return Json(new { message, employeeId, success = string.IsNullOrWhiteSpace(result.ErrorMessage) });
        }
        [HttpGet]
        public JsonResult DeleteUserPersonTask(string taskId)
        {
            int id;
            int.TryParse(taskId, out id);
            if (id > 0)
            {
                var result = _employeeFacadeApiClient.DeleteUserTask(id).Result;
                var model = result.IsSuccessStatusCode ? "Deleted successfully." : "API error occured.";
                return Json(new { message = model, taskId, success = result.IsSuccessStatusCode });
            }
            return Json(new { message = "Task removed from calendar successfully.", taskId, success = true });
        }
    }
}