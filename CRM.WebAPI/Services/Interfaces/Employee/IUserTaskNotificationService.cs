using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserTaskNotificationService
    {
        Task<UserTaskNotificationDto> GetAsync(int id);
        Task<int> SaveAsync(UserTaskNotificationDto userCalendarTaskNotification);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserTaskNotificationDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserTaskNotificationDto>> GetAsync();
        Task<bool> UserCalendarTaskNotificationExistsAsync(int id);
        PageResult<UserTaskNotificationDto> GetAllAsync(ODataQueryOptions<UserTaskNotification> options);
    }
}