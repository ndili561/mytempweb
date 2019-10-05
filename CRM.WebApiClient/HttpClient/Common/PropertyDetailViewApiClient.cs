using System;
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
    public class PropertyDetailViewApiClient : BaseClient, IPropertyDetailViewApiClient
    {
        public PropertyDetailViewApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PropertyDetailViewDto> GetPropertyDetailView(string propertyCode)
        {
            var url = CRMApiUri + "/PropertyDetailView?propertyCode=" + propertyCode;
            var result = await GetResultFromApi<PropertyDetailViewDto>(url);
            return result;
        }

        

        public async Task<PropertyDetailViewSearchModel> GetPropertyDetailViews(PropertyDetailViewSearchModel model)
        {
            var url = ODataApiUri + "/PropertyDetailView?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PropertyDetailViewSearchResult.Clear();
            model.PropertyDetailViewSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PropertyDetailViewDto>(item.ToString())));
            return model;
        }

        private string GetFilterString(PropertyDetailViewSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.PropertyCode))
                {
                    filterString = ODataFilterConstant.Filter + $" PropertyCode eq '{searchModel.PropertyCode}'";
                }
                else
                {
                    filterString = ODataFilterConstant.Filter + " PropertyCode ne ''";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.PropertyType))
                {
                    filterString += $" and PropertyType eq '{searchModel.PropertyCode}'";

                }
                if (searchModel.NumberOfBedRooms.HasValue && searchModel.NumberOfBedRooms > 0)
                {
                    filterString += $" and Bedrooms eq {searchModel.NumberOfBedRooms}";

                }
                if (!string.IsNullOrWhiteSpace(searchModel.Postcode))
                {
                    filterString += $" and PostCode eq '{searchModel.Postcode}'";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.CurrentMainTenant))
                {
                    filterString += $" and contains(CurrentMainTenant,'{searchModel.CurrentMainTenant}') eq true";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.Address))
                {
                    filterString+= $" and( contains(AddressLine1,'{searchModel.Address}') eq true";
                    filterString += $" or contains(AddressLine2,'{searchModel.Address}') eq true";
                    filterString += $" or contains(AddressLine3,'{searchModel.Address}') eq true";
                    filterString += $" or contains(AddressLine4,'{searchModel.Address}') eq true )";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
