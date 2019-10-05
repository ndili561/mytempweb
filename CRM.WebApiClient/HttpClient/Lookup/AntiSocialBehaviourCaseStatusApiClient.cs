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
    public class AntiSocialBehaviourCaseStatusApiClient : BaseClient, IAntiSocialBehaviourCaseStatusApiClient
    {
        public AntiSocialBehaviourCaseStatusApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> GetAntiSocialBehaviourCaseStatus(int alertTypeId)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourCaseStatus/" + alertTypeId;
            var result = await GetResultFromApi<AntiSocialBehaviourCaseStatusDto>(url);
            return result;
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> PostAntiSocialBehaviourCaseStatus(AntiSocialBehaviourCaseStatusDto model)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourCaseStatus";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> PutAntiSocialBehaviourCaseStatus(int id,AntiSocialBehaviourCaseStatusDto model)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourCaseStatus/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AntiSocialBehaviourCaseStatusSearchModel> GetAntiSocialBehaviourCaseStatuses(AntiSocialBehaviourCaseStatusSearchModel model)
        {
            var url = ODataApiUri + "/AntiSocialBehaviourCaseStatus?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AntiSocialBehaviourCaseStatusSearchResult.Clear();
            model.AntiSocialBehaviourCaseStatusSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AntiSocialBehaviourCaseStatusDto>(item.ToString())));
            return model;
        }

    }
}
