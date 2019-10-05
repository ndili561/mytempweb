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
    public class RelationshipApiClient : BaseClient, IRelationshipApiClient
    {
        public RelationshipApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<RelationshipDto> GetRelationship(int languageId)
        {
            var url = CRMApiUri + "/Relationship/" + languageId;
            var result = await GetResultFromApi<RelationshipDto>(url);
            return result;
        }

        public async Task<RelationshipDto> PostRelationship(RelationshipDto model)
        {
            var url = CRMApiUri + "/Relationship";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<RelationshipDto> PutRelationship(int id,RelationshipDto model)
        {
            var url = CRMApiUri + "/Relationship/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<RelationshipSearchModel> GetRelationships(RelationshipSearchModel model)
        {
            var url = ODataApiUri + "/Relationship?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.RelationshipSearchResult.Clear();
            model.RelationshipSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<RelationshipDto>(item.ToString())));
            return model;
        }
    }
}
