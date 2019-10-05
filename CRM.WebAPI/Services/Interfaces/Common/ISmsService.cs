using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Common
{
    public interface ISmsService
    {
        Task<SmsDto> GetAsync(int id);
        Task<int> SaveAsync(SmsDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(SmsDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SmsDto>> GetAsync();
        Task<bool> SmsExistsAsync(int id);
        PageResult<SmsDto> GetAllAsync(ODataQueryOptions<Sms> options);
    }
}