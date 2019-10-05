using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IApplicationRoleService
    {
        Task<ApplicationRoleDto> GetAsync(int id);
        Task<int> SaveAsync(ApplicationRoleDto applicationRole);
        Task<BaseEntity> SaveAndReturnEntityAsync(ApplicationRoleDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ApplicationRoleDto>> GetAsync();
        Task<bool> ApplicationRoleExistsAsync(int id);
        PageResult<ApplicationRoleDto> GetAllAsync(ODataQueryOptions<ApplicationRole> options);
    }
}