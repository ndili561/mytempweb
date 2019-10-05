using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Employee;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserPersonTaskCalendarFileService
    {
        Task<UserDto> GetAsync(int id);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserDto entityDto);
     
    }
}