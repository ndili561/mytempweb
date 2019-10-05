using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IPropertyDocumentApiClient
    {
        Task<PropertyDocumentSearchModel> GetPropertyDocuments(PropertyDocumentSearchModel model);
        Task<PropertyDocumentDto> GetPropertyDocument(int id);
        Task<PropertyDocumentDto> PostPropertyDocument(PropertyDocumentDto model);
        Task<PropertyDocumentDto> PutPropertyDocument(int id, PropertyDocumentDto model);
        Task<HttpResponseMessage> DeletePropertyDocument(int id);

    }
}