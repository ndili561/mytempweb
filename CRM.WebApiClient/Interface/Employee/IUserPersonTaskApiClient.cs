using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserPersonTaskApiClient
    {
        Task<UserTaskSearchModel> GetUserTasks(UserTaskSearchModel model);
        Task<UserPersonTaskDto> GetUserTask(int userTaskId);
        Task<UserDto> GetUserTaskCalendarFile(int employeeId);
        Task<UserDto> PutUserTaskCalendarFileUpload(int employeeId,UserDto model);
        Task<UserPersonTaskDto> PostUserTask(UserPersonTaskDto model);
        Task<UserPersonTaskDto> PutUserTask(int id, UserPersonTaskDto model);
        Task<HttpResponseMessage> DeleteUserTask(int id);
    }
}