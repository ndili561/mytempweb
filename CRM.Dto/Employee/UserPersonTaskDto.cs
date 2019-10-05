using System;
using CRM.Dto.Lookup;

namespace CRM.Dto.Employee
{
    public class UserPersonTaskDto : BaseDto
    {
        public string LegacyTaskId { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public int? TaskId { get; set; }
        public TaskDto Task { get; set; }
        public int TaskTypeId { get; set; }
        public TaskTypeDto TaskType { get; set; }
        public int TaskStatusId { get; set; }
        public TaskStatusDto TaskStatus { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

    }
}
