using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonContactDetailService
    {
        Task<PersonContactDetailDto> GetAsync(int id);
        Task<int> SaveAsync(PersonContactDetailDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonContactDetailDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonContactDetailDto>> GetAsync();
        Task<bool> PersonContactDetailExistsAsync(int id);
        PageResult<PersonContactDetailDto> GetAllAsync(ODataQueryOptions<PersonContactDetail> options);
    }
}