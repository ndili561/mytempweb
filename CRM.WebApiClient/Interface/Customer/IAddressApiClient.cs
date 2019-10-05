using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IAddressApiClient
    {
        Task<AddressSearchModel> GetAddresses(AddressSearchModel model);
        Task<AddressDto> GetAddress(int addressId);
        Task<AddressDto> PostAddress(AddressDto model);
        Task<AddressDto> PutAddress(int id, AddressDto model);
    }
}