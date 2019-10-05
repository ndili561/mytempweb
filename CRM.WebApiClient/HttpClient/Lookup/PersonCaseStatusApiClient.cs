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
    public class PersonCaseStatusApiClient : BaseClient, IPersonCaseStatusApiClient
    {
        public PersonCaseStatusApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonCaseStatusDto> GetCaseStatus(int id)
        {
            var url = CRMApiUri + "/PersonCaseStatus/" + id;
            var result = await GetResultFromApi<PersonCaseStatusDto>(url);
            return result;
        }

        public async Task<PersonCaseStatusDto> PostCaseStatus(PersonCaseStatusDto model)
        {
            var url = CRMApiUri + "/PersonCaseStatus";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonCaseStatusDto> PutCaseStatus(int id,PersonCaseStatusDto model)
        {
            var url = CRMApiUri + "/PersonCaseStatus/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonCaseStatusSearchModel> GetCaseStatuses(PersonCaseStatusSearchModel model)
        {
            var url = ODataApiUri + "/PersonCaseStatus?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.CaseStatusSearchResult.Clear();
            model.CaseStatusSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonCaseStatusDto>(item.ToString())));
            return model;
        }

    }
}
