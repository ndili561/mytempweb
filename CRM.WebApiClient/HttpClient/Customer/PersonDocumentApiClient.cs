using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Customer
{
    /// <summary>
    /// </summary>
    public class PersonDocumentApiClient : BaseClient, IPersonDocumentApiClient
    {
        public PersonDocumentApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonDocumentDto> GetPersonDocument(int id)
        {
            var url = CRMApiUri + "/PersonDocument/" + id;
            var result = await GetResultFromApi<PersonDocumentDto>(url);
            return result;
        }

        public async Task<PersonDocumentDto> PostPersonDocument(PersonDocumentDto model)
        {
            var url = CRMApiUri + "/PersonDocument";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonDocumentDto> PutPersonDocument(int id,PersonDocumentDto model)
        {
            var url = CRMApiUri + "/PersonDocument/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonDocumentSearchModel> GetPersonDocuments(PersonDocumentSearchModel model)
        {
            var url = ODataApiUri + "/PersonDocument?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonDocumentSearchResult.Clear();
            try
            {
                model.PersonDocumentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonDocumentDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(PersonDocumentSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.PersonId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"PersonId eq {searchModel.PersonId}";
                    if (!string.IsNullOrWhiteSpace(filterString))
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
