using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IApplicationTaskApiClient
    {
        Task<ApplicationTaskSearchModel> GetApplicationTasks(ApplicationTaskSearchModel model);
        Task<ApplicationTaskDto> GetApplicationTask(int applicationRoleId);
        Task<ApplicationTaskDto> PostApplicationTask(ApplicationTaskDto model);
        Task<ApplicationTaskDto> PutApplicationTask(int id, ApplicationTaskDto model);
    }
}