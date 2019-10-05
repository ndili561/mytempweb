using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface ITitleService
    {
        Task<TitleDto> GetAsync(int id);
        Task<int> SaveAsync(TitleDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(TitleDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TitleDto>> GetAsync();
        Task<bool> TitleExistsAsync(int id);
        PageResult<TitleDto> GetAllAsync(ODataQueryOptions<Title> options);
    }
}