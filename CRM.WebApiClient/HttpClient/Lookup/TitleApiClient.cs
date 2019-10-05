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
    public class TitleApiClient : BaseClient, ITitleApiClient
    {
        public TitleApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TitleDto> GetTitle(int titleId)
        {
            var url = CRMApiUri + "/Title/" + titleId;
            var result = await GetResultFromApi<TitleDto>(url);
            return result;
        }

        public async Task<TitleDto> PostTitle(TitleDto model)
        {
            var url = CRMApiUri + "/Title";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TitleDto> PutTitle(int id,TitleDto model)
        {
            var url = CRMApiUri + "/Title/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TitleSearchModel> GetTitles(TitleSearchModel model)
        {
            var url = ODataApiUri + "/Title?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TitleSearchResult.Clear();
            model.TitleSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TitleDto>(item.ToString())));
            return model;
        }

    }
}
