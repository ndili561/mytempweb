using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Employee
{
    public class UserPersonTaskDto : BaseDto
    {
        public UserPersonTaskDto()
        {
            EmployeeList = new List<SelectListItem>();
            PersonList = new List<SelectListItem>();
            TaskTypeList = new List<SelectListItem>();
            ApplicationList = new List<SelectListItem>();
        }
        public string LegacyTaskId { get; set; }
        
     
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        //User for view purpose only to split date and time
        [Display(Name = "Start Date")]
        public DateTime ScheduleStartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime ScheduleEndDate { get; set; }
        [Display(Name = "Start Date")]
        public DateTime? ActualStartDate { get; set; }
        [Display(Name = "Time")]
        public string ScheduleStartTimeString { get; set; }
        [Display(Name = "Time")]
        public string ScheduleEndTimeString { get; set; }
        [Display(Name = "Time")]
        public string ActualStartTimeString { get; set; }
        [Display(Name = "Time")]
        public string ActualEndTimeString { get; set; }
        public string TaskTypeSelected { get; set; }
        public string TaskStatusId { get; set; }
        public TaskStatusDto TaskStatus { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

        [DisplayName("Employee")]
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }

        [DisplayName("Person")]
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
        public List<SelectListItem> PersonList { get; set; }

        [DisplayName("Task Type")]
        public int TaskTypeId { get; set; }
        public TaskTypeDto TaskType { get; set; }
        public List<SelectListItem> TaskTypeList { get; set; }

        [DisplayName("Task")]
        public int? TaskId { get; set; }
        public TaskDto Task { get; set; }
        public List<SelectListItem> TaskList { get; set; }

        [DisplayName(" Application")]
        public int ApplicationId { get; set; }
        public List<SelectListItem> ApplicationList { get; set; }
        public string TaskName { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

    }
}
