using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IEthnicityService
    {
        Task<EthnicityDto> GetAsync(int id);
        Task<int> SaveAsync(EthnicityDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(EthnicityDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EthnicityDto>> GetAsync();
        Task<bool> EthnicityExistsAsync(int id);
        PageResult<EthnicityDto> GetAllAsync(ODataQueryOptions<Ethnicity> options);
    }
}