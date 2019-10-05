using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserDiaryService
    {
        Task<UserDiaryDto> GetAsync(int id);
        Task<int> SaveAsync(UserDiaryDto userDiary);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserDiaryDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserDiaryDto>> GetAsync();
        Task<bool> UserDiaryExistsAsync(int id);
        PageResult<UserDiaryDto> GetAllAsync(ODataQueryOptions<UserDiary> options);
    }
}