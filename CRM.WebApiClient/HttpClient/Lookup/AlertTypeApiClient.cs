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
    public class AlertTypeApiClient : BaseClient, IAlertTypeApiClient
    {
        public AlertTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AlertTypeDto> GetAlertType(int alertTypeId)
        {
            var url = CRMApiUri + "/AlertType/" + alertTypeId;
            var result = await GetResultFromApi<AlertTypeDto>(url);
            return result;
        }

        public async Task<AlertTypeDto> PostAlertType(AlertTypeDto model)
        {
            var url = CRMApiUri + "/AlertType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AlertTypeDto> PutAlertType(int id,AlertTypeDto model)
        {
            var url = CRMApiUri + "/AlertType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AlertTypeSearchModel> GetAlertTypes(AlertTypeSearchModel model)
        {
            var url = ODataApiUri + "/AlertType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AlertTypeSearchResult.Clear();
            model.AlertTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AlertTypeDto>(item.ToString())));
            return model;
        }

    }
}
