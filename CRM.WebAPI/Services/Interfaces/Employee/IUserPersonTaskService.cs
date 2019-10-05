using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserPersonTaskService
    {
        Task<UserPersonTaskDto> GetAsync(int id);
        Task<int> SaveAsync(UserPersonTaskDto userCalendarTask);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserPersonTaskDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserPersonTaskDto>> GetAsync();
        Task<bool> UserCalendarTaskExistsAsync(int id);
        PageResult<UserPersonTaskDto> GetAllAsync(ODataQueryOptions<UserPersonTask> options);
    }
}