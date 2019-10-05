using System;
using CRM.Dto.Lookup;

namespace CRM.Dto.Employee
{
    public class UserTaskReminderDto : BaseDto
    {
        public int UserPersonTaskId { get; set; }
        public UserPersonTaskDto UserPersonTask { get; set; }
        public int TaskReminderTypeId { get; set; }
        public TaskReminderTypeDto TaskReminderType { get; set; }
        public bool ReminderSent { get; set; }
        public DateTime? NextReminderDue { get; set; }
    }
}
