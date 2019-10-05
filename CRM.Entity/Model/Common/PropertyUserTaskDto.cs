using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Common
{
    public class PropertyUserTaskDto : BaseDto
    {
        public string LegacyTaskId { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int PropertyId { get; set; }
        public PropertyDto Property { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public int? PropertyVoidId { get; set; }
        public PropertyVoidDto PropertyVoid { get; set; }
        public int? TaskId { get; set; }
        public TaskDto Task { get; set; }
        public int TaskTypeId { get; set; }
        public TaskTypeDto TaskType { get; set; }
        public int TaskStatusId { get; set; }
        public TaskStatusDto TaskStatus { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

    }
}
