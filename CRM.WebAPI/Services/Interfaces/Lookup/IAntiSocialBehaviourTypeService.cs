using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAntiSocialBehaviourTypeService
    {
        Task<AntiSocialBehaviourTypeDto> GetAsync(int id);
        Task<int> SaveAsync(AntiSocialBehaviourTypeDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(AntiSocialBehaviourTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AntiSocialBehaviourTypeDto>> GetAsync();
        Task<bool> AntiSocialBehaviourTypeExistsAsync(int id);
    }
}