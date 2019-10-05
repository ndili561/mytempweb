using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonDocumentService
    {
        Task<PersonDocumentDto> GetAsync(int id);
        Task<int> SaveAsync(PersonDocumentDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonDocumentDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonDocumentDto>> GetAsync();
        Task<bool> PersonDocumentExistsAsync(int id);
    }
}