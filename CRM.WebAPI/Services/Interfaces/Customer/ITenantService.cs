using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface ITenantService
    {
        Task<TenantDto> GetAsync(int id);
        Task<int> SaveAsync(TenantDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(TenantDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TenantDto>> GetAsync();
        Task<bool> TenantExistsAsync(int id);
    }
}