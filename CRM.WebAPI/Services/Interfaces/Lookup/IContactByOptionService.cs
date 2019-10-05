using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IContactByOptionService
    {
        Task<ContactByOptionDto> GetAsync(int id);
        Task<int> SaveAsync(ContactByOptionDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(ContactByOptionDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ContactByOptionDto>> GetAsync();
        Task<bool> ContactByOptionExistsAsync(int id);
        PageResult<ContactByOptionDto> GetAllAsync(ODataQueryOptions<ContactByOption> options);
    }
}