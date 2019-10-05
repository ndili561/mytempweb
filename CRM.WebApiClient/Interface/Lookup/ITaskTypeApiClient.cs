using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ITaskTypeApiClient
    {
        Task<TaskTypeSearchModel> GetTaskTypes(TaskTypeSearchModel model);
        Task<TaskTypeDto> GetTaskType(int taskReminderTypeId);
        Task<TaskTypeDto> PostTaskType(TaskTypeDto model);
        Task<TaskTypeDto> PutTaskType(int id, TaskTypeDto model);
    }
}