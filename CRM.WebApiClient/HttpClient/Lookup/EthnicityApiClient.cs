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
    public class EthnicityApiClient : BaseClient, IEthnicityApiClient
    {
        public EthnicityApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<EthnicityDto> GetEthnicity(int ethnicityId)
        {
            var url = CRMApiUri + "/Ethnicity/" + ethnicityId;
            var result = await GetResultFromApi<EthnicityDto>(url);
            return result;
        }

        public async Task<EthnicityDto> PostEthnicity(EthnicityDto model)
        {
            var url = CRMApiUri + "/Ethnicity";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<EthnicityDto> PutEthnicity(int id,EthnicityDto model)
        {
            var url = CRMApiUri + "/Ethnicity/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<EthnicitySearchModel> GetEthnicities(EthnicitySearchModel model)
        {
            var url = ODataApiUri + "/Ethnicity?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.EthnicitySearchResult.Clear();
            model.EthnicitySearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<EthnicityDto>(item.ToString())));
            return model;
        }

    }
}
