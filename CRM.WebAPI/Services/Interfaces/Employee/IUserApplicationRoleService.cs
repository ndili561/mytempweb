using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserApplicationRoleService
    {
        Task<UserApplicationRoleDto> GetAsync(int id);
        Task<int> SaveAsync(UserApplicationRoleDto userApplicationRole);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserApplicationRoleDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserApplicationRoleDto>> GetAsync();
        Task<bool> UserApplicationRoleExistsAsync(int id);
        PageResult<UserApplicationRoleDto> GetAllAsync(ODataQueryOptions<UserApplicationRole> options);
    }
}