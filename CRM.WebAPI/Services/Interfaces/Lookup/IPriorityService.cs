using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IPriorityService
    {
        Task<PriorityDto> GetAsync(int id);
        Task<int> SaveAsync(PriorityDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(PriorityDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PriorityDto>> GetAsync();
        Task<bool> PriorityExistsAsync(int id);
    }
}