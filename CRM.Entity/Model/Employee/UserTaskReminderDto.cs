using System;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Employee
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
