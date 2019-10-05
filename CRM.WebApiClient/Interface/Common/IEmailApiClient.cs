using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IEmailApiClient
    {
        Task<EmailSearchModel> GetEmails(EmailSearchModel model);
        Task<EmailDto> GetEmail(int id);
        Task<EmailDto> PostEmail(EmailDto model);
        Task<EmailDto> PutEmail(int id, EmailDto model);
    }
}