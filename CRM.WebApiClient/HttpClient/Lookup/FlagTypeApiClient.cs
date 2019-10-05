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
    public class FlagTypeApiClient : BaseClient, IFlagTypeApiClient
    {
        public FlagTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<FlagTypeDto> GetFlagType(int flagTypeId)
        {
            var url = CRMApiUri + "/FlagType/" + flagTypeId;
            var result = await GetResultFromApi<FlagTypeDto>(url);
            return result;
        }

        public async Task<FlagTypeDto> PostFlagType(FlagTypeDto model)
        {
            var url = CRMApiUri + "/FlagType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<FlagTypeDto> PutFlagType(int id,FlagTypeDto model)
        {
            var url = CRMApiUri + "/FlagType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<FlagTypeSearchModel> GetFlagTypes(FlagTypeSearchModel model)
        {
            var url = ODataApiUri + "/FlagType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.FlagTypeSearchResult.Clear();
            model.FlagTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<FlagTypeDto>(item.ToString())));
            return model;
        }
    }
}
