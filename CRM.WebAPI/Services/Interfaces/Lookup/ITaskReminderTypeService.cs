using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface ITaskReminderTypeService
    {
        Task<TaskReminderTypeDto> GetAsync(int id);
        Task<int> SaveAsync(TaskReminderTypeDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(TaskReminderTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskReminderTypeDto>> GetAsync();
        Task<bool> TaskReminderTypeExistsAsync(int id);
        PageResult<TaskReminderTypeDto> GetAllAsync(ODataQueryOptions<TaskReminderType> options);
    }
}