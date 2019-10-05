using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Common
{
    /// <summary>
    /// </summary>
    public class DocumentApiClient : BaseClient, IDocumentApiClient
    {
        public DocumentApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<DocumentDto> GetDocument(int id)
        {
            var url = CRMApiUri + "/Document/" + id;
            var result = await GetResultFromApi<DocumentDto>(url);
            return result;
        }

        public async Task<FileMetadata> GetDocumentFile(FileMetadata model)
        {
            var url = DocumentApiUri + "FileManagement/DecryptAndGetFile";
            var result = await PostRequestToApi(url,model);
            return result;
        }

        public async Task<DocumentDto> PostDocument(DocumentDto model)
        {
            var url = CRMApiUri + "/Document";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<DocumentDto> PutDocument(int id,DocumentDto model)
        {
            var url = CRMApiUri + "/Document/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<DocumentSearchModel> GetDocuments(DocumentSearchModel model)
        {
            var url = ODataApiUri + "/Document?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.DocumentSearchResult.Clear();
            model.DocumentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<DocumentDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(DocumentSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Forename,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString += $" or contains(Forename,'{searchModel.FilterText}') eq true";
                    }
                    filterString += $" or contains(Surname,'{searchModel.FilterText}') eq true";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
