using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IChatMessageApiClient
    {
        Task<ChatMessageSearchModel> GetChatMessages(ChatMessageSearchModel model);
        Task<ChatMessageDto> GetChatMessage(int ChatMessageId);
        Task<ChatMessageDto> PostChatMessage(ChatMessageDto model);
        Task<ChatMessageDto> PutChatMessage(int id, ChatMessageDto model);
    }
}