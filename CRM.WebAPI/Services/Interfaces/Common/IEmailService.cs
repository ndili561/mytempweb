using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Common
{
    public interface IEmailService
    {
        Task<EmailDto> GetAsync(int id);
        Task<int> SaveAsync(EmailDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(EmailDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EmailDto>> GetAsync();
        Task<bool> EmailExistsAsync(int id);
        PageResult<EmailDto> GetAllAsync(ODataQueryOptions<Email> options);
    }
}