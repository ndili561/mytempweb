using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonApplicationLinkService
    {
        Task<PersonApplicationLinkDto> GetAsync(int id);
        Task<int> SaveAsync(PersonApplicationLinkDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonApplicationLinkDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonApplicationLinkDto>> GetAsync();
        Task<bool> PersonApplicationLinkExistsAsync(int id);
        PageResult<PersonApplicationLinkDto> GetAllAsync(ODataQueryOptions<PersonApplicationLink> options);
    }
}