using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserTaskNotificationApiClient
    {
        Task<UserTaskNotificationSearchModel> GetUserTaskNotifications(UserTaskNotificationSearchModel model);
        Task<UserTaskNotificationDto> GetUserTaskNotification(int id);
        Task<UserTaskNotificationDto> PostUserTaskNotification(UserTaskNotificationDto model);
        Task<UserTaskNotificationDto> PutUserTaskNotification(int id, UserTaskNotificationDto model);
    }
}