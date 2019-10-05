using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ITaskReminderTypeApiClient
    {
        Task<TaskReminderTypeSearchModel> GetTaskReminderTypes(TaskReminderTypeSearchModel model);
        Task<TaskReminderTypeDto> GetTaskReminderType(int taskReminderTypeId);
        Task<TaskReminderTypeDto> PostTaskReminderType(TaskReminderTypeDto model);
        Task<TaskReminderTypeDto> PutTaskReminderType(int id, TaskReminderTypeDto model);
    }
}