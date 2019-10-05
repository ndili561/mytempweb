using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.Extensions.Options;

namespace CRM.WebApiClient.HttpClient.Lookup
{
    /// <summary>
    /// </summary>
    public class LookupApiClient : BaseClient, ILookupApiClient
    {
        public LookupApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<LookupDto> GetLookup()
        {
            var url = CRMApiUri + "/Lookup";
            var result = await GetResultFromApi<LookupDto>(url);
            return result;
        }

        public async Task<LookupSearch> GetLookupUsingOdata(List<string> entities)
        {
            var url = ODataApiUri + "/Lookup?" + GetFilterStringForAssociatedEntities(entities); 
            var result = await GetResultFromApi<LookupSearch>(url);
            return result;
        }
        
    }
}
