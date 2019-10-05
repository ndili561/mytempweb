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
    public class AntiSocialBehaviourTypeApiClient : BaseClient, IAntiSocialBehaviourTypeApiClient
    {
        public AntiSocialBehaviourTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AntiSocialBehaviourTypeDto> GetAntiSocialBehaviourType(int alertGroupId)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourType/" + alertGroupId;
            var result = await GetResultFromApi<AntiSocialBehaviourTypeDto>(url);
            return result;
        }

        public async Task<AntiSocialBehaviourTypeDto> PostAntiSocialBehaviourType(AntiSocialBehaviourTypeDto model)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AntiSocialBehaviourTypeDto> PutAntiSocialBehaviourType(int id,AntiSocialBehaviourTypeDto model)
        {
            var url = CRMApiUri + "/AntiSocialBehaviourType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AntiSocialBehaviourTypeSearchModel> GetAntiSocialBehaviourTypes(AntiSocialBehaviourTypeSearchModel model)
        {
            var url = ODataApiUri + "/AntiSocialBehaviourType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AntiSocialBehaviourTypeSearchResult.Clear();
            model.AntiSocialBehaviourTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AntiSocialBehaviourTypeDto>(item.ToString())));
            return model;
        }
    }
}
