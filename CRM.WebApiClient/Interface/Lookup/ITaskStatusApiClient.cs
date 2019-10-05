using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ITaskStatusApiClient
    {
        Task<TaskStatusSearchModel> GetTaskStatuses(TaskStatusSearchModel model);
        Task<TaskStatusDto> GetTaskStatus(int taskStatusId);
        Task<TaskStatusDto> PostTaskStatus(TaskStatusDto model);
        Task<TaskStatusDto> PutTaskStatus(int id, TaskStatusDto model);
    }
}