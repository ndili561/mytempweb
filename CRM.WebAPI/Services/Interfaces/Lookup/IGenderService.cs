using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IGenderService
    {
        Task<GenderDto> GetAsync(int id);
        Task<int> SaveAsync(GenderDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(GenderDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<GenderDto>> GetAsync();
        Task<bool> GenderExistsAsync(int id);
        PageResult<GenderDto> GetAllAsync(ODataQueryOptions<Gender> options);
    }
}