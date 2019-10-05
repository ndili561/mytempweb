using System;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserTaskNotification : BaseEntity
    {
        public int UserTaskId { get; set; }
        public UserPersonTask UserPersonTask { get; set; }
        public int TaskNotifyTypeId { get; set; }
        public TaskNotificationType TaskNotifyType { get; set; }
        public bool NotificationSent { get; set; }
        public DateTime? NotificationSentOn { get; set; }
    }
}
