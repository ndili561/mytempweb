using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IApplicationRoleApiClient
    {
        Task<ApplicationRoleSearchModel> GetApplicationRoles(ApplicationRoleSearchModel model);
        Task<ApplicationRoleDto> GetApplicationRole(int applicationRoleId);
        Task<ApplicationRoleDto> PostApplicationRole(ApplicationRoleDto model);
        Task<ApplicationRoleDto> PutApplicationRole(int id, ApplicationRoleDto model);
    }
}