using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserTaskReminderApiClient
    {
        Task<UserTaskReminderSearchModel> GetUserTaskReminders(UserTaskReminderSearchModel model);
        Task<UserTaskReminderDto> GetUserTaskReminder(int entityId);
        Task<UserTaskReminderDto> PostUserTaskReminder(UserTaskReminderDto model);
        Task<UserTaskReminderDto> PutUserTaskReminder(int id, UserTaskReminderDto model);
    }
}