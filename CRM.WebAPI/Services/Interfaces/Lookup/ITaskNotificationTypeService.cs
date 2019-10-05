using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface ITaskNotificationTypeService
    {
        Task<TaskNotificationTypeDto> GetAsync(int id);
        Task<int> SaveAsync(TaskNotificationTypeDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(TaskNotificationTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskNotificationTypeDto>> GetAsync();
        Task<bool> TaskNotificationTypeExistsAsync(int id);
        PageResult<TaskNotificationTypeDto> GetAllAsync(ODataQueryOptions<TaskNotificationType> options);
    }
}