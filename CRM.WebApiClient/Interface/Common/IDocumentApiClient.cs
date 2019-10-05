using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IDocumentApiClient
    {
        Task<DocumentSearchModel> GetDocuments(DocumentSearchModel model);
        Task<DocumentDto> GetDocument(int id);
        Task<FileMetadata> GetDocumentFile(FileMetadata model);
        Task<DocumentDto> PostDocument(DocumentDto model);
        Task<DocumentDto> PutDocument(int id, DocumentDto model);
    }
}