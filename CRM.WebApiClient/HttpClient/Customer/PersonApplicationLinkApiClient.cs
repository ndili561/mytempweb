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
    public class PersonApplicationLinkApiClient : BaseClient, IPersonApplicationLinkApiClient
    {
        public PersonApplicationLinkApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonApplicationLinkDto> GetPersonApplicationLink(int PersonApplicationLinkId)
        {
            var url = CRMApiUri + "/People/" + PersonApplicationLinkId;
            var result = await GetResultFromApi<PersonApplicationLinkDto>(url);
            return result;
        }

        public async Task<PersonApplicationLinkDto> PostPersonApplicationLink(PersonApplicationLinkDto model)
        {
            var url = CRMApiUri + "/PersonApplicationLink";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonApplicationLinkDto> PutPersonApplicationLink(int id,PersonApplicationLinkDto model)
        {
            var url = CRMApiUri + "/PersonApplicationLink/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonApplicationLinkSearchModel> GetPersonApplicationLinks(PersonApplicationLinkSearchModel model)
        {
            var url = ODataApiUri + "/PersonApplicationLink?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonApplicationLinkSearchResult.Clear();
            model.PersonApplicationLinkSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonApplicationLinkDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonApplicationLinkSearchModel searchModel)
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
