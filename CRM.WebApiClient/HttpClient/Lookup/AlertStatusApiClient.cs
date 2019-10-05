using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
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
    public class AlertStatusApiClient : BaseClient, IAlertStatusApiClient
    {
        public AlertStatusApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AlertStatusDto> GetAlertStatus(int id)
        {
            var url = CRMApiUri + "/AlertStatus/" + id;
            var result = await GetResultFromApi<AlertStatusDto>(url);
            return result;
        }

        public async Task<AlertStatusDto> PostAlertStatus(AlertStatusDto model)
        {
            var url = CRMApiUri + "/AlertStatus";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AlertStatusDto> PutAlertStatus(int id,AlertStatusDto model)
        {
            var url = CRMApiUri + "/AlertStatus/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AlertStatusSearchModel> GetAlertStatuses(AlertStatusSearchModel model)
        {
            var url = ODataApiUri + "/AlertStatus?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AlertStatusSearchResult.Clear();
            model.AlertStatusSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AlertStatusDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(AlertStatusSearchModel searchModel)
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
