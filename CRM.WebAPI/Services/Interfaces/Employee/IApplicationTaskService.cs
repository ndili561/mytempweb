using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IApplicationTaskService
    {
        Task<ApplicationTaskDto> GetAsync(int id);
        Task<int> SaveAsync(ApplicationTaskDto applicationRole);
        Task<BaseEntity> SaveAndReturnEntityAsync(ApplicationTaskDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ApplicationTaskDto>> GetAsync();
        Task<bool> ApplicationTaskExistsAsync(int id);
        PageResult<ApplicationTaskDto> GetAllAsync(ODataQueryOptions<ApplicationTask> options);
    }
}