using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IAlertStatusService
    {
        Task<AlertStatusDto> GetAsync(int id);
        Task<int> SaveAsync(AlertStatusDto entityDto);
        Task<BaseEntity> SaveAndReturnEntityAsync(AlertStatusDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AlertStatusDto>> GetAsync();
        Task<bool> AlertStatusExistsAsync(int id);
    }
}