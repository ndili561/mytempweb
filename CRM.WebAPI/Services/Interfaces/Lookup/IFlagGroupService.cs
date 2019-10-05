using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IFlagGroupService
    {
        Task<FlagGroupDto> GetAsync(int id);
        Task<int> SaveAsync(FlagGroupDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(FlagGroupDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<FlagGroupDto>> GetAsync();
        Task<bool> FlagGroupExistsAsync(int id);
    }
}