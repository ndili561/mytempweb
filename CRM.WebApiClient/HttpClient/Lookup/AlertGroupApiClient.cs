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
    public class AlertGroupApiClient : BaseClient, IAlertGroupApiClient
    {
        public AlertGroupApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AlertGroupDto> GetAlertGroup(int alertGroupId)
        {
            var url = CRMApiUri + "/AlertGroup/" + alertGroupId;
            var result = await GetResultFromApi<AlertGroupDto>(url);
            return result;
        }

        public async Task<AlertGroupDto> PostAlertGroup(AlertGroupDto model)
        {
            var url = CRMApiUri + "/AlertGroup";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AlertGroupDto> PutAlertGroup(int id,AlertGroupDto model)
        {
            var url = CRMApiUri + "/AlertGroup/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AlertGroupSearchModel> GetAlertGroups(AlertGroupSearchModel model)
        {
            var url = ODataApiUri + "/AlertGroup?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AlertGroupSearchResult.Clear();
            model.AlertGroupSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AlertGroupDto>(item.ToString())));
            return model;
        }
    }
}
