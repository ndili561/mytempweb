using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Common
{
    public interface IDocumentService
    {
        Task<DocumentDto> GetAsync(int id);
        Task<int> SaveAsync(DocumentDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(DocumentDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DocumentDto>> GetAsync();
        Task<bool> DocumentExistsAsync(int id);
        PageResult<DocumentDto> GetAllAsync(ODataQueryOptions<Document> options);
    }
}