using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserApplicationRoleApiClient
    {
        Task<UserApplicationRoleSearchModel> GetUserApplicationRoles(UserApplicationRoleSearchModel model);
        Task<UserApplicationRoleDto> GetUserApplicationRole(int UserApplicationRoleId);
        Task<UserApplicationRoleDto> PostUserApplicationRole(UserApplicationRoleDto model);
        Task<UserApplicationRoleDto> PutUserApplicationRole(int id, UserApplicationRoleDto model);
    }
}