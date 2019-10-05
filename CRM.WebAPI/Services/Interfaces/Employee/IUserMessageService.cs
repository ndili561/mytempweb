using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IUserMessageService
    {
        Task<UserMessageDto> GetAsync(int id);
        Task<int> SaveAsync(UserMessageDto userMessage);
        Task<BaseEntity> SaveAndReturnEntityAsync(UserMessageDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserMessageDto>> GetAsync();
        Task<bool> UserMessageExistsAsync(int id);
        PageResult<UserMessageDto> GetAllAsync(ODataQueryOptions<UserMessage> options);
    }
}