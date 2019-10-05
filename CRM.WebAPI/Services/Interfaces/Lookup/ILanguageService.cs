using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface ILanguageService
    {
        Task<LanguageDto> GetAsync(int id);
        Task<int> SaveAsync(LanguageDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(LanguageDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<LanguageDto>> GetAsync();
        Task<bool> LanguageExistsAsync(int id);
        PageResult<LanguageDto> GetAllAsync(ODataQueryOptions<Language> options);
    }
}