using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IContactTypeService
    {
        Task<ContactTypeDto> GetAsync(int id);
        Task<int> SaveAsync(ContactTypeDto addressType);
        Task<BaseEntity> SaveAndReturnEntityAsync(ContactTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ContactTypeDto>> GetAsync();
        Task<bool> ContactTypeExistsAsync(int id);
    }
}