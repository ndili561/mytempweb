using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IMessageService
    {
        Task<MessageDto> GetAsync(int id);
        Task<int> SaveAsync(MessageDto message);
        Task<BaseEntity> SaveAndReturnEntityAsync(MessageDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MessageDto>> GetAsync();
        Task<bool> MessageExistsAsync(int id);
        PageResult<MessageDto> GetAllAsync(ODataQueryOptions<Message> options);
    }
}