using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserApiClient
    {
        Task<UserSearchModel> GetUsersWithTasks(UserSearchModel model);
        Task<UserSearchModel> GetUsers(UserSearchModel model);
        Task<UserDto> GetUser(int userId);
        Task<UserDto> PostUser(UserDto model);
        Task<UserDto> PutUser(int id, UserDto model);
        Task<UserDto> PatchUserTasks(int id, UserDto model);
    }
}