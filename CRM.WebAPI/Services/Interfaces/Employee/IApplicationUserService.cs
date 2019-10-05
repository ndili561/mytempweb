using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDto> GetAsync(int id);
        Task<int> SaveAsync(ApplicationUserDto applicationUser);
        Task<BaseEntity> SaveAndReturnEntityAsync(ApplicationUserDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ApplicationUserDto>> GetAsync();
        Task<bool> ApplicationUserExistsAsync(int id);
        PageResult<ApplicationUserDto> GetAllAsync(ODataQueryOptions<ApplicationUser> options);
    }
}