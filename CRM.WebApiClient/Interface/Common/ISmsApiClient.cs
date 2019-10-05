using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface ISmsApiClient
    {
        Task<SmsSearchModel> GetSmses(SmsSearchModel model);
        Task<SmsDto> GetSms(int id);
        Task<SmsDto> PostSms(SmsDto model);
        Task<SmsDto> PutSms(int id, SmsDto model);
    }
}