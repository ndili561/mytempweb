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
    public class PriorityApiClient : BaseClient, IPriorityApiClient
    {
        public PriorityApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PriorityDto> GetPriority(int flagGroupId)
        {
            var url = CRMApiUri + "/Priority/" + flagGroupId;
            var result = await GetResultFromApi<PriorityDto>(url);
            return result;
        }

        public async Task<PriorityDto> PostPriority(PriorityDto model)
        {
            var url = CRMApiUri + "/Priority";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PriorityDto> PutPriority(int id,PriorityDto model)
        {
            var url = CRMApiUri + "/Priority/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PrioritySearchModel> GetPriorities(PrioritySearchModel model)
        {
            var url = ODataApiUri + "/Priority?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PrioritySearchResult.Clear();
            model.PrioritySearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PriorityDto>(item.ToString())));
            return model;
        }
    }
}
