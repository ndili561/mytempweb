using System;
using System.Linq;
using System.Net.Http;
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
    public class PropertyDocumentApiClient : BaseClient, IPropertyDocumentApiClient
    {
        public PropertyDocumentApiClient(IOptions<HttpClientSettings> httpClientSettings):base(httpClientSettings)
        {
        }

        public async Task<PropertyDocumentDto> GetPropertyDocument(int id)
        {
            var url = CRMApiUri + "/PropertyDocument/" + id;
            var result = await GetResultFromApi<PropertyDocumentDto>(url);
            return result;
        }

        public async Task<PropertyDocumentDto> PostPropertyDocument(PropertyDocumentDto model)
        {
            var url = CRMApiUri + "/PropertyDocument";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PropertyDocumentDto> PutPropertyDocument(int id, PropertyDocumentDto model)
        {
            var url = CRMApiUri + "/PropertyDocument/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<HttpResponseMessage> DeletePropertyDocument(int id)
        {
            var url = CRMApiUri + "/PropertyDocument/" + id;
            var result = await DeleteRequestToApi(url);
            return result;
        }

        public async Task<PropertyDocumentSearchModel> GetPropertyDocuments(PropertyDocumentSearchModel model)
        {
            var url = ODataApiUri + "/PropertyDocument?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PropertyDocumentSearchResult.Clear();
            model.PropertyDocumentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PropertyDocumentDto>(item.ToString())));
            return model;
        }



        private string GetFilterString(PropertyDocumentSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.DocumentTypeId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"Document/DocumentTypeId eq {searchModel.DocumentTypeId}";
                }
                else
                {
                    filterString = ODataFilterConstant.Filter + "Document/DocumentTypeId ne 1";
                }
                if (searchModel.PropertyId > 0)
                {
                    filterString += $" and PropertyId eq {searchModel.PropertyId}";
                    if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                    {
                        filterString += $" and contains(Comment,'{searchModel.FilterText}') eq true";
                    }
                    filterString += ODataFilterConstant.Expand + $"Document";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
