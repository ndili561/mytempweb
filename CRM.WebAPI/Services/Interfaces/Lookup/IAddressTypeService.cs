using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAddressTypeService
    {
        Task<AddressTypeDto> GetAsync(int id);
        Task<int> SaveAsync(AddressTypeDto addressType);
        Task<BaseEntity> SaveAndReturnEntityAsync(AddressTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AddressTypeDto>> GetAsync();
        Task<bool> AddressTypeExistsAsync(int id);
    }
}