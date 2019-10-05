using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonApplicationLinkApiClient
    {
        Task<PersonApplicationLinkSearchModel> GetPersonApplicationLinks(PersonApplicationLinkSearchModel model);
        Task<PersonApplicationLinkDto> GetPersonApplicationLink(int personApplicationLinkId);
        Task<PersonApplicationLinkDto> PostPersonApplicationLink(PersonApplicationLinkDto model);
        Task<PersonApplicationLinkDto> PutPersonApplicationLink(int id, PersonApplicationLinkDto model);
    }
}