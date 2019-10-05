using CRM.Dto.Lookup;

namespace CRM.Dto.Employee
{
    public class UserTaskNotificationDto : BaseDto
    {
        public int UserPersonTaskId { get; set; }
        public UserPersonTaskDto UserPersonTask { get; set; }
        public int TaskNotifyTypeId { get; set; }
        public TaskNotificationTypeDto TaskNotifyType { get; set; }
        public bool NotificationSent { get; set; }
    }
}
