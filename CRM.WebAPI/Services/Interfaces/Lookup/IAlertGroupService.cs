using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAlertGroupService
    {
        Task<AlertGroupDto> GetAsync(int id);
        Task<int> SaveAsync(AlertGroupDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(AlertGroupDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AlertGroupDto>> GetAsync();
        Task<bool> AlertGroupExistsAsync(int id);
    }
}