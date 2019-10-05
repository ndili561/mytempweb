using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ITaskNotificationTypeApiClient
    {
        Task<TaskNotificationTypeSearchModel> GetTaskNotificationTypes(TaskNotificationTypeSearchModel model);
        Task<TaskNotificationTypeDto> GetTaskNotificationType(int taskNotificationTypeId);
        Task<TaskNotificationTypeDto> PostTaskNotificationType(TaskNotificationTypeDto model);
        Task<TaskNotificationTypeDto> PutTaskNotificationType(int id, TaskNotificationTypeDto model);
    }
}