using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IAddressService
    {
        Task<AddressDto> GetAsync(int id);
        Task<int> SaveAsync(AddressDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(AddressDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AddressDto>> GetAsync();
        Task<bool> AddressExistsAsync(int id);
        PageResult<AddressDto> GetAllAsync(ODataQueryOptions<Address> options);
    }
}