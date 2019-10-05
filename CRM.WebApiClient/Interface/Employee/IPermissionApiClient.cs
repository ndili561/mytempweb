using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IPermissionApiClient
    {
        Task<PermissionSearchModel> GetPermissions(PermissionSearchModel model);
        Task<PermissionDto> GetPermission(int id);
        Task<PermissionDto> PostPermission(PermissionDto model);
        Task<PermissionDto> PutPermission(int id, PermissionDto model);
    }
}