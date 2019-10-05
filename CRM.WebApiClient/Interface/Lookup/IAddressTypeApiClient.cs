using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAddressTypeApiClient
    {
        Task<AddressTypeSearchModel> GetAddressTypes(AddressTypeSearchModel model);
        Task<AddressTypeDto> GetAddressType(int addressTypeId);
        Task<AddressTypeDto> PostAddressType(AddressTypeDto model);
        Task<AddressTypeDto> PutAddressType(int id, AddressTypeDto model);
    }
}