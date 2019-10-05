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
    public class GenderApiClient : BaseClient, IGenderApiClient
    {
        public GenderApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<GenderDto> GetGender(int genderId)
        {
            var url = CRMApiUri + "/Gender/" + genderId;
            var result = await GetResultFromApi<GenderDto>(url);
            return result;
        }

        public async Task<GenderDto> PostGender(GenderDto model)
        {
            var url = CRMApiUri + "/Gender";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<GenderDto> PutGender(int id,GenderDto model)
        {
            var url = CRMApiUri + "/Gender/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<GenderSearchModel> GetGenders(GenderSearchModel model)
        {
            var url = ODataApiUri + "/Gender?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.GenderSearchResult.Clear();
            model.GenderSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<GenderDto>(item.ToString())));
            return model;
        }

    }
}
