using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonAddressApiClient
    {
        Task<PersonAddressSearchModel> GetPersonAddresses(PersonAddressSearchModel model);
        Task<PersonAddressDto> GetPersonAddress(int personAddressId);
        Task<PersonAddressDto> PostPersonAddress(PersonAddressDto model);
        Task<PersonAddressDto> PutPersonAddress(int id, PersonAddressDto model);
    }
}