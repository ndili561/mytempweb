using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserTaskReminderService
    {
        Task<UserTaskReminderDto> GetAsync(int id);
        Task<int> SaveAsync(UserTaskReminderDto userCalendarTaskReminder);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserTaskReminderDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserTaskReminderDto>> GetAsync();
        Task<bool> UserCalendarTaskReminderExistsAsync(int id);
        PageResult<UserTaskReminderDto> GetAllAsync(ODataQueryOptions<UserTaskReminder> options);
    }
}