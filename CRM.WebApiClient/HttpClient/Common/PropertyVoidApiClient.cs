using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Common
{
    /// <summary>
    /// </summary>
    public class PropertyVoidApiClient : BaseClient, IPropertyVoidApiClient
    {
        public PropertyVoidApiClient(IOptions<HttpClientSettings> httpClientSettings):base(httpClientSettings)
        {
        }



        public async Task<PropertyVoidSearchModel> GetPropertyVoids(PropertyVoidSearchModel model)
        {
            var url = ODataApiUri + "/Tenant?" + GetFilterStringForPropertyVoid(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PropertyVoidSearchResult.Clear();
            model.PropertyVoidSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PropertyVoidDto>(item.ToString())));
            return model;
        }
        private string GetFilterStringForPropertyVoid(PropertyVoidSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.PropertyCode))
                {
                    filterString = ODataFilterConstant.Filter + $"Property/PropertyCode eq '{searchModel.PropertyCode}'";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }

        public async Task<PropertyVoidViewSearchModel> GetPropertyVoidViews(PropertyVoidViewSearchModel model)
        {
            var url = ODataApiUri + "/PropertyVoidView?" + GetFilterStringForPropertyVoidView(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PropertyVoidViewSearchResult.Clear();
            model.PropertyVoidViewSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PropertyVoidViewDto>(item.ToString())));
            return model;
        }

        private string GetFilterStringForPropertyVoidView(PropertyVoidViewSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.PropertyCode))
                {
                    filterString = ODataFilterConstant.Filter + $"PropertyCode eq '{searchModel.PropertyCode}'";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
