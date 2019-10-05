using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Customer
{
    /// <summary>
    /// </summary>
    public class TenantHistoryViewApiClient : BaseClient, ITenantHistoryViewApiClient
    {
        public TenantHistoryViewApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TenantHistoryViewSearchModel> GetTenantHistoryViews(TenantHistoryViewSearchModel model)
        {
            var url = ODataApiUri + "/TenantHistoryView?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TenantHistoryViewSearchResult.Clear();
            model.TenantHistoryViewSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TenantHistoryViewDto>(item.ToString())));
            return model;
        }

        private string GetFilterString(TenantHistoryViewSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                filterString = ODataFilterConstant.Filter + $"TenantCode eq '{searchModel.TenantCode}'";
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
