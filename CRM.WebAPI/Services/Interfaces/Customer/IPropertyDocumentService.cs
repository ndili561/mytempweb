using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Common;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPropertyDocumentService
    {
        Task<PropertyDocumentDto> GetAsync(int id);
        Task<int> SaveAsync(PropertyDocumentDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PropertyDocumentDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PropertyDocumentDto>> GetAsync();
        Task<bool> PropertyDocumentExistsAsync(int id);
    }
}