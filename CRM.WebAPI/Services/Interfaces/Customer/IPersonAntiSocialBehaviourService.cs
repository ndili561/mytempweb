using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonAntiSocialBehaviourService
    {
        Task<PersonAntiSocialBehaviourDto> GetAsync(int id);
        Task<int> SaveAsync(PersonAntiSocialBehaviourDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonAntiSocialBehaviourDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonAntiSocialBehaviourDto>> GetAsync();
        Task<bool> PersonAntiSocialBehaviourExistsAsync(int id);
    }
}