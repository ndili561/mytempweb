using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonEmailApiClient
    {
        Task<PersonEmailSearchModel> GetPersonEmails(PersonEmailSearchModel model);
        Task<PersonEmailDto> GetPersonEmail(int id);
        Task<PersonEmailDto> PostPersonEmail(PersonEmailDto model);
        Task<PersonEmailDto> PutPersonEmail(int id, PersonEmailDto model);
    }
}