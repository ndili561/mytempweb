using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Employee;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IRoleService
    {
        Task<RoleDto> GetAsync(int id);
        Task<int> SaveAsync(RoleDto role);
        Task<BaseEntity> SaveAndReturnEntityAsync(RoleDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RoleDto>> GetAsync();
        Task<bool> RoleExistsAsync(int id);
    }
}