using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonFlagService
    {
        Task<PersonFlagDto> GetAsync(int id);
        Task<int> SaveAsync(PersonFlagDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonFlagDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonFlagDto>> GetAsync();
        Task<bool> PersonFlagExistsAsync(int id);
    }
}