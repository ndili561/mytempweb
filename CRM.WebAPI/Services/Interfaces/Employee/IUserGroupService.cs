using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserGroupService
    {
        Task<UserGroupDto> GetAsync(int id);
        Task<int> SaveAsync(UserGroupDto userGroup);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserGroupDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserGroupDto>> GetAsync();
        Task<bool> UserGroupExistsAsync(int id);
        PageResult<UserGroupDto> GetAllAsync(ODataQueryOptions<UserGroup> options);
    }
}