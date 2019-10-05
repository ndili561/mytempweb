using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface ITaskTypeService
    {
        Task<TaskTypeDto> GetAsync(int id);
        Task<int> SaveAsync(TaskTypeDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(TaskTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskTypeDto>> GetAsync();
        Task<bool> TaskTypeExistsAsync(int id);
        PageResult<TaskTypeDto> GetAllAsync(ODataQueryOptions<TaskType> options);
    }
}