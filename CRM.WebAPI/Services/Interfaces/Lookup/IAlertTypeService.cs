using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAlertTypeService
    {
        Task<AlertTypeDto> GetAsync(int id);
        Task<int> SaveAsync(AlertTypeDto model);
        Task<BaseEntity> SaveAndReturnEntityAsync(AlertTypeDto model);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AlertTypeDto>> GetAsync();
        Task<bool> AlertTypeExistsAsync(int id);
    }
}