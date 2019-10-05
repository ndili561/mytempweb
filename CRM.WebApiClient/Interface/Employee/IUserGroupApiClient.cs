using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserGroupApiClient
    {
        Task<UserGroupSearchModel> GetUserGroups(UserGroupSearchModel model);
        Task<UserGroupDto> GetUserGroup(int UserGroupId);
        Task<UserGroupDto> PostUserGroup(UserGroupDto model);
        Task<UserGroupDto> PutUserGroup(int id, UserGroupDto model);
    }
}