using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IFlagTypeService
    {
        Task<FlagTypeDto> GetAsync(int id);
        Task<int> SaveAsync(FlagTypeDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(FlagTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<FlagTypeDto>> GetAsync();
        Task<bool> FlagTypeExistsAsync(int id);
    }
}