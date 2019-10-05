using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Task = CRM.DAL.Database.Entities.Employee.Task;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface ITaskService
    {
        Task<TaskDto> GetAsync(int id);
        Task<int> SaveAsync(TaskDto calendarTask);
        Task<BaseEntity> SaveAndReturnEntityAsync(TaskDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskDto>> GetAsync();
        Task<bool> TaskExistsAsync(int id);
        PageResult<TaskDto> GetAllAsync(ODataQueryOptions<Task> options);
    }
}