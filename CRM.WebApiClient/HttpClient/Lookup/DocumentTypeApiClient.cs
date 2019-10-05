using System.Linq;
using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Lookup
{
    /// <summary>
    /// </summary>
    public class DocumentTypeApiClient : BaseClient, IDocumentTypeApiClient
    {
        public DocumentTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<DocumentTypeDto> GetDocumentType(int documentTypeId)
        {
            var url = CRMApiUri + "/DocumentType/" + documentTypeId;
            var result = await GetResultFromApi<DocumentTypeDto>(url);
            return result;
        }

        public async Task<DocumentTypeDto> PostDocumentType(DocumentTypeDto model)
        {
            var url = CRMApiUri + "/DocumentType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<DocumentTypeDto> PutDocumentType(int id,DocumentTypeDto model)
        {
            var url = CRMApiUri + "/DocumentType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<DocumentTypeSearchModel> GetDocumentTypes(DocumentTypeSearchModel model)
        {
            var url = ODataApiUri + "/DocumentType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.DocumentTypeSearchResult.Clear();
            model.DocumentTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<DocumentTypeDto>(item.ToString())));
            return model;
        }

    }
}
