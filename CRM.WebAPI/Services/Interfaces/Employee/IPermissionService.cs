using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IPermissionService
    {
        Task<PermissionDto> GetAsync(int id);
        Task<int> SaveAsync(PermissionDto permission);
        Task<BaseEntity> SaveAndReturnEntityAsync(PermissionDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PermissionDto>> GetAsync();
        Task<bool> PermissionExistsAsync(int id);
        PageResult<PermissionDto> GetAllAsync(ODataQueryOptions<Permission> options);
    }
}