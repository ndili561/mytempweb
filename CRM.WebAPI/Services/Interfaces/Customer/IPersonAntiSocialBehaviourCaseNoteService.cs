using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonAntiSocialBehaviourCaseNoteService
    {
        Task<PersonAntiSocialBehaviourCaseNoteDto> GetAsync(int id);
        Task<int> SaveAsync(PersonAntiSocialBehaviourCaseNoteDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonAntiSocialBehaviourCaseNoteDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonAntiSocialBehaviourCaseNoteDto>> GetAsync();
        Task<bool> PersonAntiSocialBehaviourCaseNoteExistsAsync(int id);
    }
}