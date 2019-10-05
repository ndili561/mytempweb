using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface ITaskApiClient
    {
        Task<TaskSearchModel> GetTasks(TaskSearchModel model);
        Task<TaskDto> GetTask(int id);
        Task<TaskDto> PostTask(TaskDto model);
        Task<TaskDto> PutTask(int id, TaskDto model);
    }
}