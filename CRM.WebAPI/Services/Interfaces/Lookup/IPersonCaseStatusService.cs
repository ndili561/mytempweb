using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IPersonCaseStatusService
    {
        Task<PersonCaseStatusDto> GetAsync(int id);
        Task<int> SaveAsync(PersonCaseStatusDto entityDto);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonCaseStatusDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonCaseStatusDto>> GetAsync();
        Task<bool> CaseStatusExistsAsync(int id);
    }
}