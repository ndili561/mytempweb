using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserActivityService
    {
        Task<UserActivityDto> GetAsync(int id);
        Task<int> SaveAsync(UserActivityDto userActivity);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserActivityDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserActivityDto>> GetAsync();
        Task<bool> UserActivityExistsAsync(int id);
        PageResult<UserActivityDto> GetAllAsync(ODataQueryOptions<UserActivity> options);
    }
}