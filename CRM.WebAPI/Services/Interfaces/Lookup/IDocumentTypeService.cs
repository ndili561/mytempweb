using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IDocumentTypeService
    {
        Task<DocumentTypeDto> GetAsync(int id);
        Task<int> SaveAsync(DocumentTypeDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(DocumentTypeDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DocumentTypeDto>> GetAsync();
        Task<bool> DocumentTypeExistsAsync(int id);
        PageResult<DocumentTypeDto> GetAllAsync(ODataQueryOptions<DocumentType> options);
    }
}