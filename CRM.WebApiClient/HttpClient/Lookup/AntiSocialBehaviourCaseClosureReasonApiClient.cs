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
    public class AntiSocialBehaviourCaseClosureReasonApiClient : BaseClient, IAntiSocialBehaviourCaseClosureReasonApiClient
    {
        public AntiSocialBehaviourCaseClosureReasonApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> GetAntiSocialBehaviourCaseClosureReason(int alertTypeId)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourCaseClosureReason/" + alertTypeId;
            var result = await GetResultFromApi<AntiSocialBehaviourCaseClosureReasonDto>(url);
            return result;
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> PostAntiSocialBehaviourCaseClosureReason(AntiSocialBehaviourCaseClosureReasonDto model)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourCaseClosureReason";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> PutAntiSocialBehaviourCaseClosureReason(int id,AntiSocialBehaviourCaseClosureReasonDto model)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourCaseClosureReason/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonSearchModel> GetAntiSocialBehaviourCaseClosureReasons(AntiSocialBehaviourCaseClosureReasonSearchModel model)
        {
            var url = ODataApiUri + "/AntiSocialBehaviourCaseClosureReason?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AntiSocialBehaviourCaseClosureReasonSearchResult.Clear();
            model.AntiSocialBehaviourCaseClosureReasonSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AntiSocialBehaviourCaseClosureReasonDto>(item.ToString())));
            return model;
        }

    }
}
