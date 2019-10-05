using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserMessageApiClient
    {
        Task<UserMessageSearchModel> GetUserMessages(UserMessageSearchModel model);
        Task<UserMessageDto> GetUserMessage(int userMessageId);
        Task<UserMessageDto> PostUserMessage(UserMessageDto model);
        Task<UserMessageDto> PutUserMessage(int id, UserMessageDto model);
    }
}