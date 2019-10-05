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
    public class PersonCaseTypeApiClient : BaseClient, IPersonCaseTypeApiClient
    {
        public PersonCaseTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonCaseTypeDto> GetCaseType(int id)
        {
            var url = CRMApiUri + "/PersonCaseType/" + id;
            var result = await GetResultFromApi<PersonCaseTypeDto>(url);
            return result;
        }

        public async Task<PersonCaseTypeDto> PostCaseType(PersonCaseTypeDto model)
        {
            var url = CRMApiUri + "/PersonCaseType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonCaseTypeDto> PutCaseType(int id,PersonCaseTypeDto model)
        {
            var url = CRMApiUri + "/PersonCaseType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonCaseTypeSearchModel> GetCaseTypes(PersonCaseTypeSearchModel model)
        {
            var url = ODataApiUri + "/PersonCaseType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.CaseTypeSearchResult.Clear();
            model.CaseTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonCaseTypeDto>(item.ToString())));
            return model;
        }
    }
}
