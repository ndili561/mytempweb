using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IMergePersonService
    {
        Task<MergePersonDto> GetAsync(int id);
        Task<int> SaveAsync(MergePersonDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(MergePersonDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MergePersonDto>> GetAsync();
        Task<bool> MergePersonExistsAsync(int id);
        PageResult<MergePersonDto> GetAllAsync(ODataQueryOptions<MergePerson> options);
    }
}