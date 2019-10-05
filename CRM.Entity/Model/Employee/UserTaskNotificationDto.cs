using System;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Employee
{
    public class UserTaskNotificationDto : BaseDto
    {
        public int UserPersonTaskId { get; set; }
        public UserPersonTaskDto UserPersonTask { get; set; }
        public int TaskNotifyTypeId { get; set; }
        public TaskNotificationTypeDto TaskNotifyType { get; set; }
        public bool NotificationSent { get; set; }
        public DateTime? NotificationSentOn { get; set; }
    }
}
