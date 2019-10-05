using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using TaskStatus = CRM.DAL.Database.Entities.Lookup.TaskStatus;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface ITaskStatusService
    {
        Task<TaskStatusDto> GetAsync(int id);
        Task<int> SaveAsync(TaskStatusDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(TaskStatusDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskStatusDto>> GetAsync();
        Task<bool> TaskStatusExistsAsync(int id);
        PageResult<TaskStatusDto> GetAllAsync(ODataQueryOptions<TaskStatus> options);
    }
}