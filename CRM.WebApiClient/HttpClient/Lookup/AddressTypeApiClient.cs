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
    public class AddressTypeApiClient : BaseClient, IAddressTypeApiClient
    {
        public AddressTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AddressTypeDto> GetAddressType(int addressTypeId)
        {
            var url = CRMApiUri + "/AddressType/" + addressTypeId;
            var result = await GetResultFromApi<AddressTypeDto>(url);
            return result;
        }

        public async Task<AddressTypeDto> PostAddressType(AddressTypeDto model)
        {
            var url = CRMApiUri + "/AddressType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AddressTypeDto> PutAddressType(int id,AddressTypeDto model)
        {
            var url = CRMApiUri + "/AddressType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AddressTypeSearchModel> GetAddressTypes(AddressTypeSearchModel model)
        {
            var url = ODataApiUri + "/AddressType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AddressTypeSearchResult.Clear();
            model.AddressTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AddressTypeDto>(item.ToString())));
            return model;
        }
    }
}
