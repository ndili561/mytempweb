using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IPersonCaseTypeService
    {
        Task<PersonCaseTypeDto> GetAsync(int id);
        Task<int> SaveAsync(PersonCaseTypeDto entityDto);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonCaseTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonCaseTypeDto>> GetAsync();
        Task<bool> CaseTypeExistsAsync(int id);
    }
}