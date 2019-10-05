using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IDocumentTypeApiClient
    {
        Task<DocumentTypeSearchModel> GetDocumentTypes(DocumentTypeSearchModel model);
        Task<DocumentTypeDto> GetDocumentType(int id);
        Task<DocumentTypeDto> PostDocumentType(DocumentTypeDto model);
        Task<DocumentTypeDto> PutDocumentType(int id, DocumentTypeDto model);
    }
}