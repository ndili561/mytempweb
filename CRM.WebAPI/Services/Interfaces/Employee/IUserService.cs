using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(int id);
        Task<int> SaveAsync(UserDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserDto>> GetAsync();
        Task<bool> UserExistsAsync(int id);
        PageResult<UserDto> GetAllAsync(ODataQueryOptions<User> options);
        Task<UserDto> UpdatePatch(int id, Delta<User> userPatch);
    }
}