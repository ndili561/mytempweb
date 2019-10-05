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
    public class NationalityTypeApiClient : BaseClient, INationalityTypeApiClient
    {
        public NationalityTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<NationalityTypeDto> GetNationalityType(int NationalityTypeId)
        {
            var url = CRMApiUri + "/NationalityType/" + NationalityTypeId;
            var result = await GetResultFromApi<NationalityTypeDto>(url);
            return result;
        }

        public async Task<NationalityTypeDto> PostNationalityType(NationalityTypeDto model)
        {
            var url = CRMApiUri + "/NationalityType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<NationalityTypeDto> PutNationalityType(int id,NationalityTypeDto model)
        {
            var url = CRMApiUri + "/NationalityType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<NationalityTypeSearchModel> GetNationalityTypes(NationalityTypeSearchModel model)
        {
            var url = ODataApiUri + "/NationalityType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.NationalityTypeSearchResult.Clear();
            model.NationalityTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<NationalityTypeDto>(item.ToString())));
            return model;
        }

    }
}
