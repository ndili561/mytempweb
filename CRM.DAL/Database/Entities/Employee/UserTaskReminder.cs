using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserTaskReminder : BaseEntity
    {
        public int UserPersonTaskId { get; set; }
        [ForeignKey("UserPersonTaskId")]
        public UserPersonTask UserPersonTask { get; set; }
        public int TaskReminderTypeId { get; set; }
        [ForeignKey("TaskReminderTypeId")]
        public TaskReminderType TaskReminderType { get; set; }
        public bool ReminderSent { get; set; }
        public DateTime? NextReminderDue { get; set; }
    }
}
