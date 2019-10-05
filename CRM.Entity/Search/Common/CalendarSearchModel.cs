using System.Collections.Generic;
using System.ComponentModel;
using Core.Utilities;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Search.Common
{
    public class CalendarSearchModel : BaseFilterModel
    {
        public CalendarSearchModel()
        {
            CalendarSearchResult = new List<CalendarTask>();
            EmployeeList = new List<SelectListItem>();
            PersonList = new List<SelectListItem>();
            TaskTypeList = new List<SelectListItem>();
            ApplicationList = new List<SelectListItem>();
        }

        public List<CalendarTask> CalendarSearchResult { get; set; }
        [DisplayName("Employee")]
        public string EmployeeIds { get; set; }
        [DisplayName("Employee")]
        public int? EmployeeId { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        [DisplayName("Person")]
        public int? PersonId { get; set; }
        public List<SelectListItem> PersonList { get; set; }
        [DisplayName("Task Type")]
        public int? TaskTypeId { get; set; }
        public List<SelectListItem> TaskTypeList { get; set; }
        public List<TaskTypeDto> TaskTypes { get; set; }

        [DisplayName("Task")]
        public int? TaskId { get; set; }
        public List<SelectListItem> TaskList { get; set; }
        public List<TaskDto> Tasks { get; set; }

        [DisplayName("Application")]
        public int? ApplicationId { get; set; }
        public List<SelectListItem> ApplicationList { get; set; }
        public List<TaskTypeDto> Applications { get; set; }
    }
}