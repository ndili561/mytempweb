using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IRoleApiClient
    {
        Task<RoleSearchModel> GetRoles(RoleSearchModel model);
        Task<RoleDto> GetRole(int RoleId);
        Task<RoleDto> PostRole(RoleDto model);
        Task<RoleDto> PutRole(int id, RoleDto model);
    }
}