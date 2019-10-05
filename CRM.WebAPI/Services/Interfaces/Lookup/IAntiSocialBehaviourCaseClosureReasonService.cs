using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAntiSocialBehaviourCaseClosureReasonService
    {
        Task<AntiSocialBehaviourCaseClosureReasonDto> GetAsync(int id);
        Task<int> SaveAsync(AntiSocialBehaviourCaseClosureReasonDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(AntiSocialBehaviourCaseClosureReasonDto model);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AntiSocialBehaviourCaseClosureReasonDto>> GetAsync();
        Task<bool> AntiSocialBehaviourCaseClosureReasonExistsAsync(int id);
    }
}