using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonSmsApiClient
    {
        Task<PersonSmsSearchModel> GetPersonSmses(PersonSmsSearchModel model);
        Task<PersonSmsDto> GetPersonSms(int id);
        Task<PersonSmsDto> PostPersonSms(PersonSmsDto model);
        Task<PersonSmsDto> PutPersonSms(int id, PersonSmsDto model);
    }
}