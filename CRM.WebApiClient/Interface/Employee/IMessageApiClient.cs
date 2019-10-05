using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IMessageApiClient
    {
        Task<MessageSearchModel> GetMessages(MessageSearchModel model);
        Task<MessageDto> GetMessage(int MessageId);
        Task<MessageDto> PostMessage(MessageDto model);
        Task<MessageDto> PutMessage(int id, MessageDto model);
    }
}