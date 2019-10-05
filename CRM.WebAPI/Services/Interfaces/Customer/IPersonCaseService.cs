using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonCaseService
    {
        Task<PersonCaseDto> GetAsync(int id);
        Task<int> SaveAsync(PersonCaseDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonCaseDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonCaseDto>> GetAsync();
        Task<bool> PersonCaseExistsAsync(int id);
    }
}