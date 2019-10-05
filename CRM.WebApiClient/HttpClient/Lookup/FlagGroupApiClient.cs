using System.Linq;
using System.Threading.Tasks;
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
    public class FlagGroupApiClient : BaseClient, IFlagGroupApiClient
    {
        public FlagGroupApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<FlagGroupDto> GetFlagGroup(int flagGroupId)
        {
            var url = CRMApiUri + "/FlagGroup/" + flagGroupId;
            var result = await GetResultFromApi<FlagGroupDto>(url);
            return result;
        }

        public async Task<FlagGroupDto> PostFlagGroup(FlagGroupDto model)
        {
            var url = CRMApiUri + "/FlagGroup";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<FlagGroupDto> PutFlagGroup(int id,FlagGroupDto model)
        {
            var url = CRMApiUri + "/FlagGroup/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<FlagGroupSearchModel> GetFlagGroups(FlagGroupSearchModel model)
        {
            var url = ODataApiUri + "/FlagGroup?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.FlagGroupSearchResult.Clear();
            model.FlagGroupSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<FlagGroupDto>(item.ToString())));
            return model;
        }
    }
}
