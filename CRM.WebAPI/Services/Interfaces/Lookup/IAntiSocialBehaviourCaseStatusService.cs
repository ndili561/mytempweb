using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAntiSocialBehaviourCaseStatusService
    {
        Task<AntiSocialBehaviourCaseStatusDto> GetAsync(int id);
        Task<int> SaveAsync(AntiSocialBehaviourCaseStatusDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(AntiSocialBehaviourCaseStatusDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AntiSocialBehaviourCaseStatusDto>> GetAsync();
        Task<bool> AntiSocialBehaviourCaseStatusExistsAsync(int id);
    }
}