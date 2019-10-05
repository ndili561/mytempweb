using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface INationalityTypeService
    {
        Task<NationalityTypeDto> GetAsync(int id);
        Task<int> SaveAsync(NationalityTypeDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(NationalityTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<NationalityTypeDto>> GetAsync();
        Task<bool> NationalityExistsAsync(int id);
        PageResult<NationalityTypeDto> GetAllAsync(ODataQueryOptions<NationalityType> options);
    }
}