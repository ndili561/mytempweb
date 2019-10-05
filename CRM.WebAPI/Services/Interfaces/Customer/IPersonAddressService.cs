using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonAddressService
    {
        Task<PersonAddressDto> GetAsync(int id);
        Task<int> SaveAsync(PersonAddressDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonAddressDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonAddressDto>> GetAsync();
        Task<bool> PersonAddressExistsAsync(int id);
    }
}