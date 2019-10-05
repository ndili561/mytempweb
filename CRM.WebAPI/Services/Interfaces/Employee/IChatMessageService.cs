using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IChatMessageService
    {
        Task<ChatMessageDto> GetAsync(int id);
        Task<int> SaveAsync(ChatMessageDto chatMessage);
        Task<BaseEntity> SaveAndReturnEntityAsync(ChatMessageDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ChatMessageDto>> GetAsync();
        Task<bool> ChatMessageExistsAsync(int id);
        PageResult<ChatMessageDto> GetAllAsync(ODataQueryOptions<ChatMessage> options);
    }
}