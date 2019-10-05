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
    public class LanguageApiClient : BaseClient, ILanguageApiClient
    {
        public LanguageApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<LanguageDto> GetLanguage(int languageId)
        {
            var url = CRMApiUri + "/Language/" + languageId;
            var result = await GetResultFromApi<LanguageDto>(url);
            return result;
        }

        public async Task<LanguageDto> PostLanguage(LanguageDto model)
        {
            var url = CRMApiUri + "/Language";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<LanguageDto> PutLanguage(int id,LanguageDto model)
        {
            var url = CRMApiUri + "/Language/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<LanguageSearchModel> GetLanguages(LanguageSearchModel model)
        {
            var url = ODataApiUri + "/Language?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.LanguageSearchResult.Clear();
            model.LanguageSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<LanguageDto>(item.ToString())));
            return model;
        }
    }
}
