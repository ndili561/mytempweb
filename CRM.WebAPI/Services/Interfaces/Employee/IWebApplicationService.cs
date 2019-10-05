using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IWebApplicationService
    {
        Task<WebApplicationDto> GetAsync(int id);
        Task<int> SaveAsync(WebApplicationDto webApplication);
        Task<BaseEntity> SaveAndReturnEntityAsync(WebApplicationDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<WebApplicationDto>> GetAsync();
        Task<bool> WebApplicationExistsAsync(int id);
        PageResult<WebApplicationDto> GetAllAsync(ODataQueryOptions<WebApplication> options);
    }
}